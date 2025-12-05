using System;
using System.Collections.Generic;

namespace ManageSpacesOfInstitute
{
    public static class Shared
    {
        public static class Partial
        {
            public static List<string> info = new List<string>
            {
                "ROOM_ID",
                "ROOMNUMBER",
                "BUILDINGNAME",
                "ROOMTYPE",
                "BUILDINGTYPE",
                "ROOMPURPOSE",
                "EQUIPMENTLIST"
            };
            public static string proc = "GET_PARTIAL_ROOM_INFO";
            public static List<string> to_hide = new List<string> { "ROOM_ID" };

            public static List<string> naming = new List<string>
            {
                "ROOM_ID",
                "Номер аудитории",
                "Корпус",
                "Тип аудитории",
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
            public static string to_hide = "GET_RESPONSIBLES";
            public static List<string> proc = new List<string> { "PERSONID" };
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
                "NAME",
                "SERIAL_NUMBER",
                "QUANTITY",
                "STATUS",
                "PURCHASE_DATE",
                "NOTES"
            };
            public static string proc = "GET_EQUIPMENT_LIST";
            public static List<string> to_hide = new List<string> { "EQUIPMENTID" };
            public static List<string> naming = new List<string>
            {
                "EQUIPMENTID",
                "Название оборудования",
                "Серийный номер",
                "Количество",
                "Статус",
                "Дата постановки на учет",
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
                "ROOMPURPOSE"
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
                "Назначение аудитории"
            };
        }

        public static class Buildings
        {
            public static List<string> info = new List<string>
            {
                "BUILDINGID",
                "NAME",
                "TYPE",
                "ADRESS",
            };
            public static string proc = "GET_BUILDINGS";
            public static List<string> to_hide = new List<string> { "BUILDINGID" };
            public static List<string> naming = new List<string>
            {
                "BUILDINGID",
                "Корпус",
                "Тип корпуса",
                "Адресс"
            };
        }

        public static class Chairs
        {
            public static List<string> info = new List<string>
            {
                "ID",
                "NAME",
                "FACULTY",
            };
            public static string proc = "GET_CHAIRS";
            public static List<string> to_hide = new List<string> { "ID" };
            public static List<string> naming = new List<string>
            {
                "ID",
                "Кафедра",
                "Факультет"
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
    }
}