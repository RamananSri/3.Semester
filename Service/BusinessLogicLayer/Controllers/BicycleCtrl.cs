using System;
using System.Collections.Generic;
using DataAccessLayer;
using ModelLayer;

namespace BusinessLogicLayer
{
    public class BicycleCtrl
    {
        private readonly IBicycleDB _bicycleDB;
        private AdvertisementCtrl _adCtrl;

        public BicycleCtrl()
        {
            _bicycleDB = new BicycleDB();
        }

        public Bicycle FindBicycle(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Bicycle> GetBikesByUser(int id)
        {
            try
            {
                List<Bicycle> bikes = _bicycleDB.GetBikesByUser(id) ?? new List<Bicycle>();
                return bikes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Brand> GetBrands()
        {
            try
            {
                List<Brand> brands = _bicycleDB.GetBrands() ?? new List<Brand>();
                return brands;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BicycleType> GetTypes()
        {
            try
            {
                List<BicycleType> types = _bicycleDB.GetTypes() ?? new List<BicycleType>();
                return types;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Wheel> GetWheelSizes()
        {
            try
            {
                List<Wheel> wheels = _bicycleDB.GetWheelSizes() ?? new List<Wheel>();
                return wheels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Frame> GetFrameSizes()
        {
            try
            {
                List<Frame> frames = _bicycleDB.GetFrameSizes() ?? new List<Frame>();
                return frames;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateBicycle(Bicycle b)
        {
            _bicycleDB.CreateBicyle(b);
        }

        public Bicycle GetBikeByYear(string year)
        {
            try
            {
                return _bicycleDB.GetBikeByYear(year);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveBicycle(int bicycleId)
        {
            this._adCtrl = new AdvertisementCtrl();
            _adCtrl.RemoveAdsByBikeId(bicycleId);
            _bicycleDB.RemoveBicycle(bicycleId);
        }
    }
}