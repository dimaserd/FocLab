using System;
using System.Linq;
using Croco.Core.Utils;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Newtonsoft.Json;

namespace FocLab.Logic.Models
{

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

            File1 = new ChemistryTaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File1));
            File2 = new ChemistryTaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File2));
            File3 = new ChemistryTaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File3));
            File4 = new ChemistryTaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.File4));

            ReactionSchemaImage = new ChemistryTaskPerformerFile(t.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage));

            MethodFile = t.ChemistryMethodFile == null ? null : new Chemistry_TaskMethodFile(t.ChemistryMethodFile);
            PerformedDate = t.PerformedDate;
            PerformerId = t.PerformerUserId;
            MethodFileId = t.ChemistryMethodFile != null? int.Parse(t.ChemistryMethodFile.Id) : 0;
            PerformerQuality = t.PerformerQuality;
            PerformerQuantity = t.PerformerQuantity;
            PerformerText = t.PerformerText;
            Title = t.Title;

            Id = t.Id;
            Deleted = t.Deleted;
            Performer = new Chemistry_Task_Performer
            {
                UserId = t.PerformerUser.Id,
                Email = t.PerformerUser.Email,
                Name = t.PerformerUser.Name
            };

            ObjectJson = new ChemistryTaskObjectJson
            {
                SubstanceCounter = JsonConvert.DeserializeObject<Chemistry_SubstanceCounter>(t.SubstanceCounterJson ?? "") ?? Chemistry_SubstanceCounter.GetDefaultCounter()
            };
        }


        void SetObjectJsonProperties(string objJson)
        {
            var objectJson = Tool.JsonConverter.Deserialize<ChemistryTaskObjectJson>(objJson);

            ObjectJson = objectJson;

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

            PerformerQuantity = ObjectJson.PerformerQuantity;

            PerformerText = ObjectJson.PerformerText;

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
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskObjectJson ObjectJson { get; set; }


        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string Id { get; set; }

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
        public ChemistryTaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskPerformerFile File1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskPerformerFile File2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskPerformerFile File3 { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskPerformerFile File4 { get; set; }

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