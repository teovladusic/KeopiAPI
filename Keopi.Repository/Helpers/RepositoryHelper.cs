using Keopi.Models.Params;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Repository.Helpers
{
    public class RepositoryHelper : IRepositoryHelper
    {
        public List<BsonDocument> GetFiltersBuilder(CafeParams cafeParams)
        {
            var pipeline = new List<BsonDocument>();

            if (!string.IsNullOrEmpty(cafeParams.CityId))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("cityId", cafeParams.CityId)));
            }
            if (!string.IsNullOrEmpty(cafeParams.Name))
            {
                pipeline.Add(
                     new BsonDocument(
                         "$search", new BsonDocument("text", new BsonDocument("query", cafeParams.Name).Add("path", "name")
                         .Add("fuzzy", new BsonDocument("maxEdits", 2)))));
            }
            if (!string.IsNullOrEmpty(cafeParams.Capacity))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("capacity", cafeParams.Capacity)));
            }
            if (cafeParams.Betting ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("betting", cafeParams.Betting)));
            }
            if (!string.IsNullOrEmpty(cafeParams.AreaId))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("areaId", cafeParams.AreaId)));
            }
            if (!string.IsNullOrEmpty(cafeParams.Location))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("location", cafeParams.Location)));
            }
            if (!string.IsNullOrEmpty(cafeParams.Music))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("music", cafeParams.Music)));
            }
            if (cafeParams.Dart ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("dart", cafeParams.Dart)));
            }
            if (cafeParams.StartingWorkTime != null)
            {
                var workingHours = CalculateStartingWorkingHours((int)cafeParams.StartingWorkTime);
                pipeline.Add(
                    new BsonDocument(
                        "$match", new BsonDocument(
                            "startingWorkTime", new BsonDocument(
                                "$in", workingHours))));
            }

            if (cafeParams.EndingWorkTime != null)
            {
                var workingHours = CalculateEndingWorkingHours((int)cafeParams.EndingWorkTime);

                pipeline.Add(
                    new BsonDocument(
                        "$match", new BsonDocument(
                            "endingWorkTime", new BsonDocument(
                                "$in", workingHours))));
            }

            if (!string.IsNullOrEmpty(cafeParams.Age))
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("age", cafeParams.Age)));
            }
            if (cafeParams.Smoking ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("smoking", cafeParams.Smoking)));
            }
            if (cafeParams.Terrace ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("terrace", cafeParams.Terrace)));
            }
            if (cafeParams.Hookah ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("hookah", cafeParams.Hookah)));
            }
            if (cafeParams.Playground ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("playground", cafeParams.Playground)));
            }
            if (cafeParams.Billiards ?? false)
            {
                pipeline.Add(new BsonDocument("$match", new BsonDocument("billiards", cafeParams.Billiards)));
            }
            return pipeline;
        }

        private static BsonArray CalculateStartingWorkingHours(int startingWorkTime)
        {
            var workingHours = new BsonArray();

            if (startingWorkTime < 10)
            {
                for (int i = 0; i <= startingWorkTime; i++)
                {
                    workingHours.Add(i);
                }
            }

            return workingHours;
        }

        private static BsonArray CalculateEndingWorkingHours(int endingWorkTime)
        {
            var workingHours = new BsonArray();

            //if time is bigger then 4 and less than 19 return all cafes
            if (endingWorkTime < 8 || endingWorkTime > 19)
            {
                //if time is less then 8 then return cafes that work from 4
                if (endingWorkTime < 8)
                {
                    for (int i = endingWorkTime; i <= 8; i++)
                    {
                        workingHours.Add(i);
                    }
                }
                else if (endingWorkTime > 19)
                {
                    //if time is bigger than 19 return cafes that work from 19 to 23 and from 0 to 4
                    for (int i = endingWorkTime; i <= 23; i++)
                    {
                        workingHours.Add(i);
                    }

                    for (int i = 0; i <= 4; i++)
                    {
                        workingHours.Add(i);
                    }
                }
            }

            return workingHours;
        }
    }
}