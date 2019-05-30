using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Croco.Core.Abstractions;
using Croco.Core.Utils;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Newtonsoft.Json;

namespace FocLab.Logic.Models
{
    #region Модели

    /// <summary>
    /// Изменить файл для эксперимента
    /// </summary>
    public class Chemistry_ChangeFileForExperiment
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string ExperimentId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public IFileData File { get; set; }

        /// <summary>
        /// Если 0 это Изображение реакции, Далее Файл 1, Файл 2, Файл 3, Файл 4, 
        /// </summary>
        public Chemistry_Task_Experiment_File ExperimentFile { get; set; }
    }

    /// <summary>
    /// Изменение файла для химического задания
    /// </summary>
    public class Chemistry_ChangeFileForTask
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public IFileData File { get; set; }

        /// <summary>
        /// Если 0 это Изображение реакции, Далее Файл 1, Файл 2, Файл 3, Файл 4, 
        /// </summary>
        public Chemistry_Task_Experiment_File TaskFile { get; set; }
    }
    #endregion

    #region Сущности

    /// <summary>
    /// Химическое вещество
    /// </summary>
    public class Chemistry_Substance
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Масса
        /// </summary>
        public string Massa { get; set; }

        /// <summary>
        /// Молярная масса
        /// </summary>
        public string MolarMassa { get; set; }

        /// <summary>
        /// Коефициент
        /// </summary>
        // ReSharper disable once IdentifierTypo
        public string Koef { get; set; }
    }

    /// <summary>
    /// Счетчик веществ
    /// </summary>
    public class Chemistry_SubstanceCounter
    {
        /// <summary>
        /// Получить счетчик по-умолчанию
        /// </summary>
        /// <returns></returns>
        public static Chemistry_SubstanceCounter GetDefaultCounter()
        {
            return new Chemistry_SubstanceCounter
            {
                Etalon = new Chemistry_Substance
                {
                    Koef = 1.ToString(),
                    Massa = 1.ToString(),
                    MolarMassa = 1.ToString(),
                    Name = "Эталонное вещество"
                },

                Substances = new List<Chemistry_Substance>()
            };
        }

        /// <summary>
        /// Эталонное вещество
        /// </summary>
        // ReSharper disable once IdentifierTypo
        public Chemistry_Substance Etalon { get; set; }

        /// <summary>
        /// Вещества
        /// </summary>
        public List<Chemistry_Substance> Substances { get; set; }
    }

    /// <summary>
    /// Модель для создания химического задания
    /// </summary>
    public class Chemistry_CreateTask
    {
        /// <summary>
        /// Идентифкатор администратора
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// Идентификатор Исполнителя
        /// </summary>
        [Display(Name = "Назначить исполнителя")]
        public string PerformerId { get; set; }

        /// <summary>
        /// Условие задачи
        /// </summary>
        [Display(Name = "Условие задачи")]
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [Display(Name = "Метод решения задачи")]
        public int FileMethodId { get; set; }

        /// <summary>
        /// Колличество
        /// </summary>
        [Display(Name = "Колличество")]
        public string Quantity { get; set; }

        /// <summary>
        /// Качество
        /// </summary>
        [Display(Name = "Качество")]
        public string Quality { get; set; }

        /// <summary>
        /// Крайний срок
        /// </summary>
        [Display(Name = "Последний срок выполнения")]
        public DateTime DeadLineDate { get; set; }
    }


    #region Задание


    #endregion


    #endregion

    /// <summary>
    /// Сущность описывающая эксперимент для химической задачи, получается из мета-сущности по определенному типу
    /// </summary>
    public class Chemistry_Task_Experiment
    {
        /// <summary>
        /// Счетчик веществ
        /// </summary>
        public Chemistry_SubstanceCounter SubstanceCounter { get; set; }

        #region Файлы
        /// <summary>
        /// Изображение реакции
        /// </summary>
        public Chemistry_TaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public List<Chemistry_Task_Experiment_File> Files { get; set; }

        #endregion

        /// <summary>
        /// Идентификатор эксперимента (то есть идентификатор мета-сущности)
        /// </summary>
        public string ExperimentId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string PerformerId { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string PerformerEmail { get; set; }

        /// <summary>
        /// Текст написанный исполнителем
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// Идентификатор химического задания
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Название задания к которому назначен эксперимент
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// сериализованный обьект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJSON { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Chemistry_TaskObjectJSON
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Дата выполнения
        /// </summary>
        public DateTime? PerformedDate { get; set; }

        /// <summary>
        /// Крайняя дата выполнения
        /// </summary>
        public DateTime DeadLineDate { get; set; }

        /// <summary>
        /// Метод выполнения (ссылка на файл)
        /// </summary>
        public int? MethodFileId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public Chemistry_Task_Performer Performer { get; set; }

        /// <summary>
        /// Метод файл
        /// </summary>
        public Chemistry_TaskMethodFile MethodFile { get; set; }

        /// <summary>
        /// Счетчик веществ
        /// </summary>
        public Chemistry_SubstanceCounter SubstanceCounter { get; set; }

        /// <summary>
        /// Колличество установленное админом
        /// </summary>
        public string AdminQuantity { get; set; }

        /// <summary>
        /// Качество установленное администратором
        /// </summary>
        public string AdminQuality { get; set; }

        /// <summary>
        /// Колличество установленное исполнителем
        /// </summary>
        public string PerformerQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerformerQuality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// Эксперименты
        /// </summary>
        public List<Chemistry_Task_Experiment> Experiments { get; set; }

        #region Файлы

        /// <summary>
        /// Изображение реакции
        /// </summary>
        public Chemistry_TaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// Файл 1
        /// </summary>
        public Chemistry_TaskPerformerFile File1 { get; set; }

        /// <summary>
        /// Файл 2
        /// </summary>
        public Chemistry_TaskPerformerFile File2 { get; set; }

        /// <summary>
        /// Файл 3
        /// </summary>
        public Chemistry_TaskPerformerFile File3 { get; set; }

        /// <summary>
        /// Файл 4
        /// </summary>
        public Chemistry_TaskPerformerFile File4 { get; set; }

        #endregion
    }

    /// <summary>
    /// Химический метод файла
    /// </summary>
    public class Chemistry_TaskMethodFile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model"></param>
        public Chemistry_TaskMethodFile(ChemistryMethodFile model)
        {
            Name = model.Name;
            FileId = model.FileId;
            CreationDate = model.CreationDate;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Chemistry_TaskMethodFile()
        {

        }


        /// <summary>
        /// Название 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Получить ссылку на файл
        /// </summary>
        /// <returns></returns>
        public string GetLinkToFile()
        {
            return $"/Files/GetDbFileById/{FileId}";
        }
    }

    /// <summary>
    /// Исполнитель химического задания
    /// </summary>
    public class Chemistry_Task_Performer
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="user"></param>
        public Chemistry_Task_Performer(ApplicationUserDto user)
        {
            UserId = user.Id;
            Name = user.Name;
            Email = user.Email;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Chemistry_Task_Performer()
        {

        }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }
    }

    
    /// <summary>
    /// 
    /// </summary>
    public class Chemistry_TaskPerformerFile
    {
        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public Chemistry_TaskPerformerFile(ChemistryTaskDbFile file)
        {
            FileId = file.FileId;
        }

        /// <summary>
        /// 
        /// </summary>
        public int FileId { get; set; }
    }

    /// <summary>
    /// Файл для эксперимента 
    /// </summary>
    public class Chemistry_Task_Experiment_File
    {
        /// <summary>
        /// 
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskDbFileType FileType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Chemistry_Task
    {
        #region Конструктор
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public Chemistry_Task(ChemistryTask t)
        {
            AdminId = t.AdminUserId;
            AdminQuality = t.AdminQuality;
            AdminQuantity = t.AdminQuantity;
            CreationDate = t.CreationDate;
            DeadLineDate = t.DeadLineDate;

            File1 = new Chemistry_TaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File1));
            File2 = new Chemistry_TaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File2));
            File3 = new Chemistry_TaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File3));
            File4 = new Chemistry_TaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File4));

            ReactionSchemaImage = new Chemistry_TaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage));

            MethodFile = t.ChemistryMethodFile == null ? null : new Chemistry_TaskMethodFile(t.ChemistryMethodFile);
            PerformedDate = t.PerformedDate;
            PerformerId = t.PerformerUserId;
            MethodFileId = t.ChemistryMethodFile != null? int.Parse(t.ChemistryMethodFile.Id) : 0;
            PerformerQuality = t.PerformerQuality;
            PerformerQuantity = t.PerformerQuantity;
            PerformerText = t.PerformerText;
            Title = t.Title;

            MetaEntityId = t.Id;

            Performer = new Chemistry_Task_Performer
            {
                UserId = t.PerformerUser.Id,
                Email = t.PerformerUser.Email,
                Name = t.PerformerUser.Name
            };

            ObjectJSON = new Chemistry_TaskObjectJSON
            {
                SubstanceCounter = JsonConvert.DeserializeObject<Chemistry_SubstanceCounter>(t.SubstanceCounterJson ?? "") ?? Chemistry_SubstanceCounter.GetDefaultCounter()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_Task()
        {

        }

        
        

        

        void SetObjectJSONProperties(string objJson)
        {
            var objectJson = Tool.JsonConverter.Deserialize<Chemistry_TaskObjectJSON>(objJson);

            ObjectJSON = objectJson;

            if (objectJson == null)
            {
                return;
            }

            Title = objectJson.Title;

            PerformedDate = objectJson.PerformedDate;

            Performer = objectJson.Performer;

            MethodFile = objectJson.MethodFile;

            DeadLineDate = objectJson.DeadLineDate;

            AdminQuality = objectJson.AdminQuality;

            AdminQuantity = objectJson.AdminQuantity;

            PerformerQuality = objectJson.PerformerQuality;

            PerformerQuantity = ObjectJSON.PerformerQuantity;

            PerformerText = ObjectJSON.PerformerText;

            #region Файлы

            ReactionSchemaImage = objectJson.ReactionSchemaImage;

            File1 = objectJson.File1;

            File2 = objectJson.File2;

            File3 = objectJson.File3;

            File4 = objectJson.File4;

            #endregion
        }

        #endregion
        

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskObjectJSON ObjectJSON { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string MetaEntityId { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Дата к которой нужно выполнить задание
        /// </summary>
        public DateTime DeadLineDate { get; set; }

        /// <summary>
        /// Дата выполнения задачи исполнителем
        /// </summary>
        public DateTime? PerformedDate { get; set; }

        #region Незначащие свойства (Геттовые свойства)

        /// <summary>
        /// Выполнено
        /// </summary>
        public bool IsPerformed => PerformedDate.HasValue;

        /// <summary>
        /// Выполнено вовремя
        /// </summary>
        public bool IsPerformedInTime => PerformedDate.HasValue && DeadLineDate > PerformedDate.Value;

        #endregion

        #region Свойства индексовые 

        /// <summary>
        /// Ссылка на метод решения данной задачи (ссылка на файл в системе) (IntProperty1)
        /// </summary>
        public int MethodFileId { get; set; }


        /// <summary>
        /// Тот кто дал задание (StringProperty1)
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// Исполнитель кому назначено задание (StringProperty2)
        /// </summary>
        public string PerformerId { get; set; }


        /// <summary>
        /// Дата создания задачи администратором
        /// </summary>
        public DateTime CreationDate { get; set; }
        #endregion

        #region Файлы

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile File1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile File2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile File3 { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Chemistry_TaskPerformerFile File4 { get; set; }

        #endregion

        /// <summary>
        /// Колличество установленное администратором
        /// </summary>
        public string AdminQuantity { get; set; }

        /// <summary>
        /// Качество установленное администратором
        /// </summary>
        public string AdminQuality { get; set; }

        /// <summary>
        /// Колличество установленное исполнителем
        /// </summary>
        public string PerformerQuantity { get; set; }

        /// <summary>
        /// Качество установленное исполнителем
        /// </summary>
        public string PerformerQuality { get; set; }

        /// <summary>
        /// Текст исполнителя
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// Исполнитель задания
        /// </summary>
        public Chemistry_Task_Performer Performer { get; set; }

        /// <summary>
        /// Метод решения являющийся файлом
        /// </summary>
        public Chemistry_TaskMethodFile MethodFile { get; set; }
    }
}