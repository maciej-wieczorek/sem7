import pandas

def add_pass_column(dataset):
    dataset["passed"] = dataset["G3"].apply(lambda grade: "Yes" if grade >= 8 else "No")

student_math = pandas.read_csv("student/student-mat.csv", sep=';')
student_por = pandas.read_csv("student/student-por.csv", sep=';')

add_pass_column(student_math)
add_pass_column(student_por)

student_math.to_csv("student/student-mat-preprocessed.csv", index=False)
student_por.to_csv("student/student-por-preprocessed.csv", index=False)