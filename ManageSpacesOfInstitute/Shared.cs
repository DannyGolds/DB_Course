using System;
using System.Collections.Generic;

namespace ManageSpacesOfInstitute
{
    public static class Shared
    {
        public static void LoadImageFromBlob(PictureBox pictureBox, object blobData)
        {
            // Скрываем PictureBox, если данных нет
            pictureBox.Visible = false;

            if (blobData == null || blobData == DBNull.Value)
                return;

            try
            {
                byte[] imageData = (byte[])blobData;

                // Проверка: не пустой массив?
                if (imageData.Length == 0)
                    return;

                using (var ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // или AutoSize, StretchImage
                    pictureBox.Visible = true;
                }
            }
            catch (Exception ex)
            {
                pictureBox.Visible = false;
            }
        }
        public static class Partial
        {
            public static List<string> info = new List<string>
            {
                "ROOM_ID",
                "ROOMNUMBER",
                "BUILDINGNAME",
                "ROOMTYPE",
                "ROOMTYPEID",
                "BUILDINGTYPE",
                "ROOMPURPOSE",
                "EQUIPMENTLIST"
            };
            public static string proc = "GET_PARTIAL_ROOM_INFO";
            public static List<string> to_hide = new List<string> { "ROOM_ID", "ROOMTYPEID" };

            public static List<string> naming = new List<string>
            {
                "ROOM_ID",
                "Номер аудитории",
                "Корпус",
                "Тип аудитории",
                "ROOMTYPEID",
                "Тип корпуса",
                "Назначение аудитории",
                "Оборудование"
            };
        }

        public static class Responsible
        {
            public static List<string> info = new List<string>
            {
                "PERSONID",
                "NAME",
                "JOBPOSITION",
                "PHONE",
            };
            public static string proc = "GET_RESPONSIBLES";
            public static List<string> to_hide = new List<string> { "PERSONID" };
            public static List<string> naming = new List<string>
            {
                "PERSONID",
                "Полное имя",
                "Должность",
                "Номер телефона"
            };
        }

        public static class Equipment
        {
            public static List<string> info = new List<string>
            {
                "EQUIPMENTID",
                "IMAGE",
                "ROOMID",
                "NAME",
                "SERIAL_NUMBER",
                "NOTES"
            };
            public static string proc = "GET_EQUIPMENT_LIST";
            public static List<string> to_hide = new List<string> { "ROOMID","IMAGE", "EQUIPMENTID" };
            public static List<string> naming = new List<string>
            {
                "EQUIPMENTID",
                "IMAGE",
                "ROOMID",
                "Название оборудования",
                "Серийный номер",
                "Описание оборудования"
            };
        }

        public static class RoomFull
        {
            public static List<string> info = new List<string>
            {
                "ROOM_ID",
                "ROOMNUMBER",
                "BUILDINGNAME",
                "BUILDINGTYPE",
                "ROOMTYPE",
                "WIDTH",
                "ROOM_LENGTH",
                "CHAIR",
                "FACULTY",
                "ROOMPURPOSE",
                "RESP"
            };
            public static string proc = "GETROOMFULLINFO";
            public static List<string> to_hide = new List<string> { "ROOM_ID" };
            public static List<string> naming = new List<string>
            {
                "ROOM_ID",
                "Номер аудитории",
                "Корпус",
                "Тип корпуса",
                "Тип аудитории",
                "Ширина",
                "Длина",
                "Кафедра",
                "Факультет",
                "Назначение аудитории",
                "Ответственный"
            };
        }

        public static class Buildings
        {
            public static List<string> info = new List<string>
            {
                "BUILDINGID",
                "NAME",
                "TYPE",
                "TYPEID",
                "IMAGE",
                "ADRESS",
            };
            public static string proc = "GET_BUILDINGS";
            public static List<string> to_hide = new List<string> { "BUILDINGID", "IMAGE", "TYPEID" };
            public static List<string> naming = new List<string>
            {
                "BUILDINGID",
                "Корпус",
                "Тип корпуса",
                "TYPEID",
                "IMAGE",
                "Адрес"
            };
        }

        public static class Chairs
        {
            public static List<string> info = new List<string>
            {
                "ID",
                "NAME",
                "FACULTY",
                "FACID"
            };
            public static string proc = "GET_CHAIRS";
            public static List<string> to_hide = new List<string> { "ID", "FACID" };
            public static List<string> naming = new List<string>
            {
                "ID",
                "Кафедра",
                "Факультет",
                "FACID"
            };
        }
        public static class Faculties
        {
            public static List<string> info = new List<string>
            {
                "ID",
                "NAME",
            };
            public static string proc = "GET_FACULTIES";
            public static List<string> to_hide = new List<string> { "ID" };
            public static List<string> naming = new List<string>
            {
                "ID",
                "Факультет",
            };
        }

        public static class BuildingTypes
        {
            public static List<string> info = new List<string> { "ID", "TYPE" };
            public static string proc = "GET_BUILDING_TYPES";
            public static List<string> to_hide = new List<string> { "ID" };
            public static List<string> naming = new List<string> { "ID", "Тип корпуса" };
        }
    }
}