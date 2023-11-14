package com.example.bigdata;
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.conf.Configured;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;
import org.apache.hadoop.util.Tool;
import org.apache.hadoop.util.ToolRunner;

import java.io.IOException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class Taxi extends Configured implements Tool {

    public static void main(String[] args) throws Exception {
        int res = ToolRunner.run(new Configuration(), new Taxi(), args);
        System.exit(res);
    }

    public int run(String[] args) throws Exception {

        Configuration conf = getConf();
        conf.set("mapreduce.output.textoutputformat.separator", ",");

        Job job = Job.getInstance(conf, "Taxi");
        job.setJarByClass(this.getClass());

        FileInputFormat.addInputPath(job, new Path(args[1]));
        FileOutputFormat.setOutputPath(job, new Path(args[2]));

        job.setMapperClass(TaxiMapper.class);
        job.setCombinerClass(TaxiReducerCombiner.class);
        job.setReducerClass(TaxiReducerCombiner.class);

        job.setOutputKeyClass(Text.class);
        job.setOutputValueClass(IntWritable.class);

        return job.waitForCompletion(true) ? 0 : 1;
    }

    public static class TaxiMapper extends Mapper<LongWritable, Text, Text, IntWritable> {

        private final Text key = new Text();
        private final IntWritable value = new IntWritable();

        @Override
        public void map(LongWritable offset, Text lineText, Context context) {
            try {
                String line = lineText.toString();

                String monthStr = null;
                String yearStr = null;
                String zoneStr = null;
                String keyStr;
                int passengersInt = -1;
                int paymentTypeInt = -1;

                int i = 0;
                for (String word : line.split(",")) {
                    if (i == 1) {
                        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
                        LocalDate localDate = LocalDate.parse(word, formatter);
                        monthStr = String.format("%02d", localDate.getMonthValue());
                        yearStr = String.valueOf(localDate.getYear());
                    }
                    if (i == 3) {
                        passengersInt = Integer.parseInt(word);
                    }
                    if (i == 7) {
                        zoneStr = word;
                    }
                    if (i == 9) {
                        paymentTypeInt = Integer.parseInt(word);
                    }
                    i++;
                }

                if (paymentTypeInt == 2) {
                    keyStr = String.format("%s-%s,%s", monthStr, yearStr, zoneStr);
                    key.set(keyStr);
                    value.set(passengersInt);
                    context.write(key, value);
                }

            } catch (Exception e) {
                System.out.println("Error in map: " + e.getMessage());
            }
        }
    }

    public static class TaxiReducerCombiner extends Reducer<Text, IntWritable, Text, IntWritable> {
        private final IntWritable resultValue = new IntWritable();
        @Override
        public void reduce(Text key, Iterable<IntWritable> values, Context context) throws IOException, InterruptedException {
            int sum = 0;
            for (IntWritable val : values) {
                sum += val.get();
            }
            resultValue.set(sum);
            context.write(key, resultValue);
        }
    }
}