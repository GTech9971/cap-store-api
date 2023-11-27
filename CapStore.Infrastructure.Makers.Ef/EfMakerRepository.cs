using System;
using CapStore.Domain.Makers;
using CapStore.Infrastructure.Makers.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Makers.Ef
{
	public class EfMakerRepository : IMakerRepository
	{

		private readonly MakerDbContext _context;

		public EfMakerRepository(MakerDbContext context)
		{
			_context = context;
		}

        public async Task<Maker?> Fetch(MakerId makerId)
        {
            MakerData? data = await _context.MakerDatas
                .AsNoTracking()
                .Where(x => x.Id == makerId.Value)
                .SingleOrDefaultAsync();

            if(data == null)
            {
                return null;
            }

            return ToModel(data);
        }

        public async Task<Maker?> Fetch(MakerName makerName)
        {
            MakerData? data = await _context.MakerDatas
                .AsNoTracking()
                .Where(x => x.Name == makerName.Value)
                .SingleOrDefaultAsync();

            if(data == null)
            {
                return null;
            }

            return ToModel(data);
        }

        public async Task<Maker> Save(Maker maker)
        {
            MakerData? found = await _context.MakerDatas
                .Where(x => x.Id == maker.Id.Value)
                .SingleOrDefaultAsync();

            MakerData data;
            if(found == null)
            {
                data = new MakerData(maker);
                await _context.MakerDatas.AddAsync(data);
            }
            else
            {
                data = Transfer(maker, found);
                _context.MakerDatas.Update(data);
            }

            await _context.SaveChangesAsync();

            return ToModel(data);
        }

        private Maker ToModel(MakerData from)
        {
            return new Maker(
                new MakerId(from.Id),
                new MakerName(from.Name),
                from.Image == null
                ? null
                : new Domain.Shareds.ImageUrl(from.Image));
        }

        /// <summary>
        /// データ更新時にはインスタンスを再作成ではなく値の再代入を行わなないとエラーが発生する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private MakerData Transfer(Maker from, MakerData to)
        {
            to.Id = from.Id.Value;
            to.Name = from.Name.Value;
            to.Image = from.Image?.Value;
            return to;
        }
    }
}

