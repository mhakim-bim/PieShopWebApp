using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (! context.Pies.Any())
            {
                context.AddRange(
                 new Pie
                    {
                     Id = 1,
                     Name = "Susage",
                        ShortDescription = "Nice Pie With Meat",
                        LongDescription = "Pie For Your Big Kersh",
                        Price = 30.00M,
                        ImageThumbnailUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRnuJUs_T2WwrrK9p6936W4TDilcuabT9BeAQ&usqp=CAU",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRnuJUs_T2WwrrK9p6936W4TDilcuabT9BeAQ&usqp=CAU",
                        IsPieOfTheWeek = true
                 },
                    new Pie
                    {
                        Id = 2,
                        Name = "Cheese",
                        ShortDescription = "Nice Pie With Cheese",
                        LongDescription = "Pie For Your Big Kersh",
                        Price = 40.00M,
                        ImageThumbnailUrl = "https://www.ldiida.com/wp-content/uploads/2016/11/%D9%81%D8%B7%D8%A7%D8%A6%D8%B1-%D8%A7%D9%84%D8%AC%D8%A8%D9%86.jpg",
                        IsPieOfTheWeek = true
                    },
                    new Pie
                    {
                        Id = 3,
                        Name = "Sugar",
                        ShortDescription = "Nice Pie With Sugar",
                        LongDescription = "Pie For Your Big Kersh",
                        Price = 20.00M,
                        ImageThumbnailUrl = "https://egytrends.net/wp-content/uploads/2019/12/d8b7d8b1d98ad982d8a9-d8b9d985d984-d981d8b7d98ad8b1d8a9-d8a7d984d8afd8acd8a7d8ac-d8a7d984d985d8bad8b1d8a8d98ad8a9_5e071e171a0b6.jpeg",
                        IsPieOfTheWeek = true
                    },
                    new Pie
                    {
                       Id = 4,
                        Name = "Honey",
                        ShortDescription = "Nice Pie With Honey",
                        LongDescription = "Pie For Your Big Kersh",
                        Price = 30.00M,
                        ImageThumbnailUrl = "https://4.bp.blogspot.com/-7IhgR713ZFM/Um_zvQZrSPI/AAAAAAAAAa4/JgldeBiSIBU/s1600/1374368_548793888534283_1965549343_n.jpg",
                        IsPieOfTheWeek = true
                    });

                context.SaveChanges();
            }

        }
    }
}
