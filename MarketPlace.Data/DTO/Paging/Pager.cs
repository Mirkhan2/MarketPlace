using System;

namespace MarketPlace.Data.DTO.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageId, int allEntitesCount, int take, int howManyShowPageAfterAndBefore)
        {
            var pageCount = Convert.ToInt32(Math.Ceiling(allEntitesCount / (double)take));

            return new BasePaging
            {
                PageId = pageId,
                AllEntitiesCount = allEntitesCount,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                StartPage = pageId - howManyShowPageAfterAndBefore <= 0 ? 1 : pageId - howManyShowPageAfterAndBefore,
                EndPage = pageId + howManyShowPageAfterAndBefore > pageCount ? pageCount : pageId + howManyShowPageAfterAndBefore,
                HowManyShowPageAfterAndBefore = howManyShowPageAfterAndBefore,
                PageCount = pageCount

            };
        }
    }
}
