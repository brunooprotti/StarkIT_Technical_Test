﻿using MediatR;
using StarkIT.Domain.Models;
using System.Linq.Expressions;

namespace StarkIT.Application.Features.Name.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQuery : IRequest<ICollection<Names>>
    {
        public Expression<Func<Names,bool>> _Expression { get; set; }
        public GetNamesListFilteredQuery(Expression<Func<Names,bool>> Expression)
        {
            _Expression = Expression ?? throw new ArgumentNullException(nameof(Expression));
        }
    }
}