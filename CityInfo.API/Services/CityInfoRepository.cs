﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Contexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        #region General
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        #endregion

        #region From_CitiesLazyLoad
        public IEnumerable<City> GetCitiesAdvancedLazyLoad()
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;

            var collection = _context.Cities
                as IQueryable<City>;

            //List<City> CityList = collection.ToList();

            //for (int Count = 0; Count < CityList.Count(); Count++)
            //{
            //    for (int Count1 = 0; Count1 < CityList[Count].PointsOfInterest.Count(); Count1++)
            //    {
            //        var Test = CityList[Count].PointsOfInterest[Count1].Name;
            //    }
            //}

            collection = collection.OrderByDescending(c => c.CityLanguages.Count).ThenBy(c => c.Name);
            
            _context.ChangeTracker.LazyLoadingEnabled = false;
            
            return collection.ToList();
        }
        #endregion

        #region From_Cities1Controller
        public IEnumerable<City> GetCitiesAdvanced()
        {
            //_context.ChangeTracker.LazyLoadingEnabled = true; 

            //try
            //{
            //    if (true == includeRelations)
            //    {
            //        return _context.Cities.
            //            Include(c => c.PointsOfInterest).
            //            Include(c => c.CityLanguages).
            //            ThenInclude(l => l.Language).
            //            ToList();
            //    }
            //    //return _context.Cities.OrderBy(c => c.CityLanguages).
            //    //        ThenBy(c => c.Name).
            //    //        ToList();
            //    return _context.Cities.
            //        OrderBy(c => c.Name).
            //            ToList();
            //}
            //catch (Exception Error)
            //{
            //    var Test = 10;
            //}

            //return (null);

            var collection = _context.Cities.
                Include(c => c.PointsOfInterest).
                Include(c => c.CityLanguages).
                ThenInclude(l => l.Language) as IQueryable<City>;

            collection = collection.OrderByDescending(c => c.CityLanguages.Count).ThenBy(c => c.Name);

            return collection.ToList();
        }

        public City GetCityAdvanced(int cityId,
                             bool includePointsOfInterest = false,
                             bool includeLanguages = false)
        {
            if ((true == includePointsOfInterest) && (true == includeLanguages))
            {
                return _context.Cities.Where(c => c.Id == cityId).
                    Include(c => c.PointsOfInterest).
                    Include(c => c.CityLanguages).
                    ThenInclude(l => l.Language).
                    FirstOrDefault();
            }

            if ((true == includePointsOfInterest) && (false == includeLanguages))
            {
                return _context.Cities.Where(c => c.Id == cityId).
                    Include(c => c.PointsOfInterest).
                    FirstOrDefault();
            }

            if ((false == includePointsOfInterest) && (true == includeLanguages))
            {
                return _context.Cities.Where(c => c.Id == cityId).
                     Include(c => c.CityLanguages).
                     ThenInclude(l => l.Language).
                     FirstOrDefault();
            }

            return _context.Cities.Where(c => c.Id == cityId).
                    FirstOrDefault();
        }

        public void AddCity(City city)
        {
            _context.Cities.Add(city);
        }

        public void UpdateCity(City city)
        {
            // I andre implementatiober af ICityRepository skal der muligvis adderes kode
            // her for at få gemt opdaterede Cities.
        }

        public void DeleteCity(City city)
        {
            _context.Cities.Remove(city);
        }
        #endregion

        #region From_CitiesController_And_PointOfIntererstController
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }
        
        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }

            return _context.Cities
                    .Where(c => c.Id == cityId).FirstOrDefault();
        }
        
        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest
               .Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest
                          .Where(p => p.CityId == cityId).ToList();
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            // I andre implementatiober af ICityRepository skal der muligvis adderes kode
            // her for at få gemt opdaterede PointOfInterest for City.
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }
        #endregion
        
    }
}
