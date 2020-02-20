using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost", "EdDb", "trusted_connection=true");
            StudentController.bcConnection = sqllib;

            var student100 = StudentController.GetByPk(100);
            if (student100 == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student100);
            }


            var students = StudentController.GetAllStudents();
            foreach(var student in students) {
                Console.WriteLine(student);
            }
            sqllib.Disconnect();
        }
    }
}
