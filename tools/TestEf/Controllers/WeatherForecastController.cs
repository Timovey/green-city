using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEf.Db;
using TestEf.Extensions;

namespace TestEf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TestDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task FillHistoryAsync<TDbModel>(
           IList<TDbModel> models,
           DbContext dbContext
        )
           where TDbModel : Student
        {
            if (models == null || models.Count < 1)
                return;

            List<StudentHistory> containerHistories = new List<StudentHistory>();
            foreach (var model in models)
            {
                var history = new StudentHistory
                {
                    Fio = model.Fio,
                    ClassId = model.ClassId,
                    Info = model.Info,
                    Info2 = model.Info2,
                    IsDeleted = model.IsDeleted,
                    DataId = model.Id
                };
                history.DtChange = DateTime.UtcNow;
                history.DataId = model.Id;

                containerHistories.Add(history);
            }

            await dbContext.Set<StudentHistory>().AddRangeAsync(containerHistories);
            await dbContext.SaveChangesAsync();
        }

        [HttpPost(Name = "ClearDb")]
        public async Task ClearDb()
        {
            var allClasses = await _context.SchoolClasses.ToListAsync();
            var allStudents = await _context.Students.ToListAsync();
            var allItems = await _context.StudentItems.ToListAsync();
            _context.SchoolClasses.RemoveRange(allClasses);
            _context.Students.RemoveRange(allStudents);
            _context.StudentItems.RemoveRange(allItems);

            await _context.SaveChangesAsync();
        }

        [HttpGet(Name = "Execute")]
        public async Task Execute([FromQuery] bool create = true)
        {
            //if (true)
            //{
            var class1 = new SchoolClass { Name = "class_1" };
            var class2 = new SchoolClass { Name = "class_2" };
            await _context.SchoolClasses.AddAsync(class1);
            await _context.SchoolClasses.AddAsync(class2);
            await _context.SaveChangesAsync();

            var student1 = new Student { Fio = "Vasya", Info = "Info1", Info2 = "Kek1", ClassId = class1.Id };
            var student2 = new Student { Fio = "Petya", Info = "Info1", Info2 = "Kek1", ClassId = class1.Id };
            var student3 = new Student { Fio = "Vova", Info = "Info1", Info2 = "Kek1", ClassId = class1.Id };
            var student4 = new Student { Fio = "Masha", Info = "Info1", Info2 = "Kek2", ClassId = class2.Id };
            var student5 = new Student { Fio = "Nina", Info = "Info2", Info2 = "Kek3", ClassId = class2.Id };
            var student6 = new Student { Fio = "Nina", Info = "Info2", Info2 = "Kek3", ClassId = class2.Id };
            var student7 = new Student { Fio = "Lox", Info = "Info3", Info2 = "Kek3", ClassId = null }; //
            var student8 = new Student { Fio = "Petyx", Info = "Info3", Info2 = "Kek3", ClassId = null }; //
            await _context.Students.AddAsync(student1);
            await _context.Students.AddAsync(student2);
            await _context.Students.AddAsync(student3);
            await _context.Students.AddAsync(student4);
            await _context.Students.AddAsync(student5);
            await _context.Students.AddAsync(student6);
            await _context.Students.AddAsync(student7);
            await _context.Students.AddAsync(student8);
            await _context.SaveChangesAsync();

            var item1 = new StudentItem { ItemName = "vasya_item_1", StudentId = student1.Id };
            var item2 = new StudentItem { ItemName = "vasya_item_2", StudentId = student1.Id };
            var item3 = new StudentItem { ItemName = "vasya_item_3", StudentId = student1.Id };
            var item4 = new StudentItem { ItemName = "masha_item_1", StudentId = student4.Id };
            var item5 = new StudentItem { ItemName = "unknown_item_1", StudentId = null }; //
            var item6 = new StudentItem { ItemName = "lox_item_1", StudentId = student7.Id }; //
            await _context.StudentItems.AddAsync(item1);
            await _context.StudentItems.AddAsync(item2);
            await _context.StudentItems.AddAsync(item3);
            await _context.StudentItems.AddAsync(item4);
            await _context.StudentItems.AddAsync(item5);
            await _context.StudentItems.AddAsync(item6);
            await _context.SaveChangesAsync();
            //}

            //var query = _context.SchoolClasses
            //        .Where(x => x.Id != 10000);

            //var studentsRequired = false;

            //var result = await (
            //    from schoolSclass in query
            //    join student in _context.Students
            //    on new { Id = (int?)schoolSclass.Id, IsDeleted = 0 }
            //    equals new { Id = student.ClassId, IsDeleted = student.IsDeleted }
            //    into Students
            //    from studentJoined in Students.DefaultIfEmpty()
            //        //.DefaultIfEmpty()
            //    from studentItem in _context.StudentItems
            //        .Where(x => x.StudentId == studentJoined.Id)
            //        .DefaultIfEmpty()
            //    where studentJoined != null || !studentsRequired
            //    select new
            //    {
            //        student = studentJoined
            //    }
            //).ToListAsync();

            //var toSortList = new List<(int id, int standId, string name)>
            //{
            //    (1, 4, "first"),
            //    (2, 20, "first"),
            //    (3, 11, "first"),
            //    (4, 8, "first"),
            //    (5, 6, "first")
            //};

            //var sortedArray = new List<(int standId, string name)>
            //{
            //    (8, "stand"),
            //    (11, "stand"),
            //    (6, "stand")
            //};

            //var sortedResult = (
            //    from sortItem in sortedArray
            //    join toSortItem in toSortList
            //        on sortItem.standId equals toSortItem.standId
            //    select toSortItem
            //).ToList();

            //var updateModel = new List<(string InfoTerm, string InfoTerm2)> { ("Info1", "Kek1"), ("Info1", "Kek2"), ("Info3", "Kek3") };

            //var toUpdate = new List<Student>();

            //foreach(var el in updateModel)
            //{
            //    var toUpdateStudentsGroup = _context.Students
            //        .Where(x => x.Info.Contains(el.InfoTerm) && x.Info2.Contains(el.InfoTerm2));

            //    foreach(var student in toUpdateStudentsGroup)
            //    {
            //        student.Fio = new Random().Next(600).ToString();
            //        toUpdate.Add(student);
            //    }

            //    //toUpdate.AddRange(toUpdateStudentsGroup);
            //}

            //var student10 = new Student { Fio = "Vasya_TOUPDATE", Info = "Info1", Info2 = "Kek1" };
            //var student20 = new Student { Fio = "Petya_TOUPDATE", Info = "Info1", Info2 = "Kek1" };
            //var student30 = new Student { Fio = "Vova_TOUPDATE", Info = "Info1", Info2 = "Kek1" };

            //await _context.Students.AddAsync(student10);
            //await _context.Students.AddAsync(student20);
            //await _context.Students.AddAsync(student30);

            //await _context.SaveChangesAsync();


            //var allStudents = await _context.Students
            //    .Where(x => x.Fio.Contains("x") &&
            //    !x.ClassId.HasValue)
            //    .ToListAsync();
            //var toUpdateModel = new List<(int, string)> { (student10.Id, "student10"), (student20.Id, "student20"), (student30.Id, "student30") };

            //var toUpdateItems = (
            //    from student in allStudents
            //    join updateItem in toUpdateModel
            //        on student.Id equals updateItem.Item1
            //    select new
            //    {
            //        Student = student,
            //        UpdateName = updateItem.Item2
            //    })
            //    .Distinct();

            //foreach (var el in toUpdateItems)
            //{
            //    el.Student.Fio = el.UpdateName;
            //    toUpdate.Add(el.Student);
            //}

            var deletedClassesList = new List<int> { class1.Id, class2.Id };

            var deleteClassQuery = _context.SchoolClasses;
            //.Where(x => x.IsDeleted == false);

            var deleteStudentQuery = _context.Students;
            //.Where(x => x.IsDeleted == 0);

            var deleteStudentItemQuery = _context.StudentItems;
            //.Where(x => x.IsDeleted == false);

            var deleteClassesWithStudentsQuery = await (
                from _class in deleteClassQuery
                join student in deleteStudentQuery
                    on _class.Id equals student.ClassId into subStudentsGroup
                from subStudent in subStudentsGroup.DefaultIfEmpty()
                join studentItem in deleteStudentItemQuery
                    on subStudent.Id equals studentItem.StudentId into subStudentitemsGroup
                from subStudentItem in subStudentitemsGroup.DefaultIfEmpty()
                where (subStudent == default || subStudent.IsDeleted == 0)
                    && (subStudentItem == default || subStudentItem.IsDeleted == false)
                    && _class.IsDeleted == false
                    && deletedClassesList.Contains(_class.Id)
                select new
                {
                    Class = _class,
                    Student = subStudent,
                    StudentItem = subStudentItem
                })
                .ToListAsync();
                //.Select(x => new
                //{
                //    Class = x.Select(xx => xx.Class).FirstOrDefault(),
                //    Attaches = x.Select(xx => new { xx.Student, xx.StudentItem }).ToList()
                //})
                //.Where(x => x.Class.IsDeleted == false)
                //.Where(x => !deletedClassesList.Contains(x.Class.Id))


            //foreach (var toDeleteClass in deleteClassesWithStudentsQuery.Select(x => x.Class).DistinctBy(x => x.Id))
            //{
            //    toDeleteClass.IsDeleted = true;
            //}

            //foreach (var toDeleteStudent in deleteClassesWithStudentsQuery.Where(x => x.Student != default).Select(x => x.Student).DistinctBy(x => x.Id))
            //{
            //    toDeleteStudent.IsDeleted = 1;
            //}

            //foreach (var toDeleteStudent in deleteClassesWithStudentsQuery.Where(x => x.StudentItem != default).Select(x => x.StudentItem).DistinctBy(x => x.Id))
            //{
            //    toDeleteStudent.IsDeleted = true;
            //}

            await _context.SaveChangesAsync();

            Console.WriteLine();
            ////await _context.SaveChangesAsync();

            //await FillHistoryAsync(toUpdate, _context);
        }
    }
}