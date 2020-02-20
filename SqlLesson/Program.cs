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
            //    Id = 777,
            //    FirstName = "Kingsly",
            //    LastName = "Kingsworth",
            //    SAT = 950,
            //    GPA = 3.1,
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
            sqllib.Disconnect();
        }
    }
}
