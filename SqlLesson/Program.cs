using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost", "EdDb", "trusted_connection=true");

            MajorController.bcConnection = sqllib;

            var majors = MajorController.GetAllMajors();
            foreach(var major in majors) {
                Console.WriteLine(major);
            }

            StudentController.bcConnection = sqllib;

            //var newStudent = new Student {
            //    Id = 667,
            //    FirstName = "Chuck",
            //    LastName = "Intern of Darkness",
            //    SAT = 000,
            //    GPA = 4.0,
            //    MajorId = null
            //};
            //var success = StudentController.InsertStudent(newStudent);

            var student100 = StudentController.GetByPk(888);
            if (student100 == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student100);
            }
            //student100.FirstName = "Jackie";
            //student100.LastName = "Chan";
            //var success = StudentController.UpdateStudent(student100);
            //
            //var studentToDelete = new Student {
            //    Id = 777
            //};
            //success = StudentController.DeleteStudent(888);

            var students = StudentController.GetAllStudents();
            foreach(var student in students) {
                Console.WriteLine(student);
            }

            var m = MajorController.GetByPk(1);
            foreach (var major in majors) {
                Console.WriteLine(m);
            }
            InstructorController.bcConnection = sqllib;

            var instructors = InstructorController.GetAllInstructors();
            foreach(var instructor in instructors) {
                Console.WriteLine(instructor);
            }

            var instructPK = InstructorController.GetByPk(30);
            if (instructPK == null) {
                Console.WriteLine("Instructor not found");
            }else {
                Console.WriteLine(instructPK);
            }

            ClassController.bcConnection = sqllib;

            var classes = ClassController.GetAllClasses();
            foreach(var cla in classes) {
                Console.WriteLine(cla);
            }

            sqllib.Disconnect();
        }
    }
}
