package com.example.bigdata;
import org.apache.hadoop.conf.Configured;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.DoubleWritable;
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

public class AvgSizeStations extends Configured implements Tool {

    public static void main(String[] args) throws Exception {
        int res = ToolRunner.run(new AvgSizeStations(), args);
        System.exit(res);
    }

    public int run(String[] args) throws Exception {
        Job job = Job.getInstance(getConf(), "AvgSizeStations");
        job.setJarByClass(this.getClass());
        FileInputFormat.addInputPath(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        //DONE: set mapper and reducer class
        job.setMapperClass(AvgSizeStationMapper.class);
        job.setReducerClass(AvgSizeStationReducer.class);

        //DONE: clean up the data types on both levels: intermediate and final
        job.setMapOutputKeyClass(Text.class);
        job.setMapOutputValueClass(IntWritable.class);

        job.setOutputKeyClass(Text.class);
        job.setOutputValueClass(IntWritable.class);
        return job.waitForCompletion(true) ? 0 : 1;
    }

    public static class AvgSizeStationMapper extends Mapper<LongWritable, Text, Text, IntWritable> {

        private final Text year = new Text();
        private final IntWritable size = new IntWritable();

        public void map(LongWritable offset, Text lineText, Context context) {
            try {
                if (offset.get() != 0) {
                    String line = lineText.toString();
                    int i = 0;
                    for (String word : line
                            .split(",(?=([^\"]*\"[^\"]*\")*[^\"]*$)")) {
                        if (i == 4) {
                            year.set(word.substring(word.lastIndexOf('/') + 1,
                                    word.lastIndexOf('/') + 5));
                        }
                        if (i == 5) {
                            size.set(Integer.parseInt(word));
                        }
                        i++;
                    }
                    //DONE: write intermediate pair to the context
                    context.write(year, size);

                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static class AvgSizeStationReducer extends Reducer<Text, IntWritable, Text, DoubleWritable> {

        private final DoubleWritable resultValue = new DoubleWritable();
        Float average;
        Float count;
        int sum;

        @Override
        public void reduce(Text key, Iterable<IntWritable> values,
                           Context context) throws IOException, InterruptedException {
            average = 0f;
            count = 0f;
            sum = 0;

            Text resultKey = new Text("average station size in " + key + " was: ");

            for (IntWritable val : values) {
                sum += val.get();
                count += 1;
            }
            //DONE: set average variable properly
            average = sum / count;

            resultValue.set(average);
            //DONE: write result pair to the context
            context.write(resultKey, resultValue);
        }
    }

    public static class AvgSizeStationCombiner extends Reducer<Text, SumCount, Text, SumCount> {

        private final SumCount sum = new SumCount(0.0d, 0);

        @Override
        public void reduce(Text key, Iterable<SumCount> values, Context context) throws IOException, InterruptedException {

            sum.set(new DoubleWritable(0.0d), new IntWritable(0));

            for (SumCount val : values) {
                sum.addSumCount(val);
            }
            context.write(key, sum);
        }
    }
}