using System;
using System.Collections.Generic;
using ModelLayer;

namespace DataAccessLayer
{
    public interface IAdvertisementDB
    {
        // READ

        Advertisement FindAdById(int id);

        List<Advertisement> GetAdvertisementsByUser(int id);

        List<Advertisement> GetAllAds();

        Advertisement GetAdByTitle(string title);

        List<Advertisement> GetAvailableAds (DateTime start, DateTime end);

        // DELETE

        void RemoveAd(int id);

        void RemoveAdsByBikeId(int bicycleId);

        // UPDATE

        void ModifyAd(Advertisement a);

        // CREATE

        void AddAd(Advertisement Ad);
        
    }
}