using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dwCheckApi.DatabaseContexts;
using dwCheckApi.DatabaseTools;
using dwCheckApi.Helpers;
using dwCheckApi.Models;
using dwCheckApi.ViewModels;
using Moq;
using Xunit;
using System.IO;
using System;
using System.Diagnostics;
using Xunit.Abstractions;

namespace dwCheckApt.Tests.Helpers
{
    public static class DbSetHelpers
    {
        public static Mock<DbSet<T>> GetQueryableDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }
    }
}