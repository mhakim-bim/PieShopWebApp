using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class MockPieRepository :IPieRepository
    {
        List<Pie> _pies;

        public MockPieRepository()
        {
            if (_pies == null)
            {
                InitializePies();
            }
        }

        private void InitializePies()
        {
            var piesList = new List<Pie>
            {
                new Pie
                {
                    Id = 1, Name = "Susage", ShortDescription = "Nice Pie With Meat",
                    LongDescription = "Pie For Your Big Kersh", Price = 30.00M,
                    ImageThumbnailUrl = "https://media.gettyimages.com/photos/apple-pie-with-latticed-pastry-on-kitchen-counter-picture-id564727693?s=612x612"
                },
                new Pie
                {
                    Id = 2, Name = "Cheese", ShortDescription = "Nice Pie With Cheese",
                    LongDescription = "Pie For Your Big Kersh", Price = 40.00M,
                    ImageThumbnailUrl ="https://images-gmi-pmc.edge-generalmills.com/c8bfdd71-ed78-41a2-9a4e-f02b72be3afe.jpg"
                },
                new Pie
                {
                    Id = 3, Name = "Sugar", ShortDescription = "Nice Pie With Sugar",
                    LongDescription = "Pie For Your Big Kersh", Price = 20.00M,
                    ImageThumbnailUrl = "https://images-gmi-pmc.edge-generalmills.com/b6a2a4e7-73f5-4aec-9bb6-f2b5054d65e6.jpg"
                },
                new Pie
                {
                    Id = 4, Name = "Honey", ShortDescription = "Nice Pie With Honey",
                    LongDescription = "Pie For Your Big Kersh", Price = 30.00M,
                    ImageThumbnailUrl ="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQ_Y34f4-QN4hKvLkIoinplMN0IIhy5dmi8Kg&usqp=CAU"
                }
            };
            _pies = piesList;
        }



        public IEnumerable<Pie> GetAllPies()
        {
            return _pies;
        }

        public Pie GetPieById(int pieId)
        {
            return _pies.FirstOrDefault(p => p.Id == pieId);
        }
    }
}
