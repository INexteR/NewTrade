using Mapping;
using Model;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static User[]? users;
        public static IEnumerable<IUser> GetUsers()
        {
            return users ??= usersText.LinesToArray<User>(nameof(User.Id),
                                                          nameof(User.Surname),
                                                          nameof(User.Name),
                                                          nameof(User.Patronymic),
                                                          nameof(User.Login),
                                                          nameof(User.Password),
                                                          nameof(User.RoleId));
        }

        private const string usersText = @"
1	Лавров	Богдан	Львович	8lf0g@yandex.ru	2L6KZG	1
2	Смирнова	Полина	Фёдоровна	1zx8@yandex.ru	uzWC67	1
3	Полякова	София	Данииловна	x@mail.ru	8ntwUp	1
4	Чеботарева	Марина	Данииловна	34d@gmail.com	YOyhfR	2
5	Ермолов	Адам	Иванович	pxacl@mail.ru	RSbvHv	2
6	Васильев	Андрей	Кириллович	7o1@gmail.com	rwVDh9	2
7	Маслов	Максим	Иванович	1@gmail.com	LdNyos	3
8	Симонов	Михаил	Тимурович	iut@gmail.com	gynQMT	3
9	Павлова	Ксения	Михайловна	e3t@outlook.com	AtnDjr	3
10	Трифонов	Григорий	Юрьевич	41clb6o2g@yandex.ru	JlFRCZ	3";

    }
}
