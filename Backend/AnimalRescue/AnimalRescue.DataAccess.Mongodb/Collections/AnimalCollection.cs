﻿using AnimalRescue.DataAccess.Contracts.Interfaces;
using AnimalRescue.DataAccess.Contracts.Query;
using AnimalRescue.DataAccess.Mongodb.Models;
using AnimalRescue.DataAccess.Mongodb.QueryBuilders;
using AnimalRescue.Models.DTO.Models;

using AutoMapper;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalRescue.DataAccess.Mongodb.Collections
{
    public class AnimalCollection : BaseCollection<Animal>,
        IAnimalRepository
    {
        public AnimalCollection(
            IMongoDatabase database, 
            IQueryBuilder<Animal> queryBuilder, 
            IMapper mapper)
            : base(database, queryBuilder, mapper)
        {
        }

        public async Task<AnimalDto> CreateAnimalAsync(AnimalDto instanse)
        {
            var data = ConvertOneFrom(instanse);
            data.DateOfFound = DateTimeOffset.Now;
            var result = await base.CreateAsync(data);
            instanse = ConvertOneTo<AnimalDto>(result);

            return instanse;
        }

        public async Task DeleteAnimalAsync(string id)
        {
            await base.RemoveAsync(id);
        }

        public async Task DeleteAnimalAsync(AnimalDto instanse)
        {
            var data = ConvertOneFrom(instanse);
            await base.RemoveAsync(data);
        }

        public async Task<List<AnimalDto>> GetAnimalsAsync(int currentPage, int pageSize)
        {
            var data = await GetAsync(currentPage, pageSize);
            var result = ConvertListTo<AnimalDto>(data);

            return result;
        }

        public async Task<AnimalDto> GetAnimalAsync(string id)
        {
            var data = await base.GetAsync(id);

            var result = ConvertOneTo<AnimalDto>(data);

            return result;
        }

        public async Task UpdateAnimalAsync(AnimalDto instanse)
        {
            var newData = ConvertOneFrom(instanse);
            var oldData = await base.GetOneByIdAsync(newData.Id);
            newData.DateOfAdopted = oldData.DateOfAdopted;
            newData.DateOfFound = oldData.DateOfFound;
            await UpdateAsync(newData);
        }

        public async Task<List<AnimalDto>> GetAnimalsAsync(DbQuery query)
        {
            var data = await base.GetAsync(query);

            var result = ConvertListTo<AnimalDto>(data);

            return result;
        }

        public async Task<int> GetAnimalCountAsync(DbQuery query)
        {  
            var result = await base.GetCountAsync(query);

            return result;
        }
    }
}
