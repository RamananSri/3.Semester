using System.Collections.Generic;
using ModelLayer;

namespace DataAccessLayer
{
    public interface IBicycleDB
    {
        // READ

        Bicycle Find(int? id);

        Bicycle GetBikeByYear(string year);

        List<Bicycle> GetAllBicycles();

        List<Bicycle> GetBikesByUser(int id);

        List<Brand> GetBrands();

        List<BicycleType> GetTypes();

        List<Wheel> GetWheelSizes();

        List<Frame> GetFrameSizes();

        // DELETE

        void RemoveBicycle(int bicycleId);

        // UPDATE

//        void Modify(Bicycle A);

        // CREATE

        void CreateBicyle(Bicycle b);

    }
}