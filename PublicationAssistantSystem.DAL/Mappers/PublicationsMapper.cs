using System;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Mappers
{
    public static class PublicationsMapper
    {
        #region Convert to specific DTO
        public static PublicationBaseDTO ToDTO(this PublicationBase publication)
        {
            if (publication == null) throw new ArgumentNullException("publication");

            if (publication is Book)    return ((Book)publication).ToDTO();
            if (publication is Patent)  return ((Patent)publication).ToDTO();
            if (publication is Thesis)  return ((Thesis)publication).ToDTO();
            if (publication is Dataset) return ((Dataset)publication).ToDTO();
            if (publication is Article) return ((Article)publication).ToDTO();
            if (publication is TechnicalReport) return ((TechnicalReport)publication).ToDTO();
            if (publication is ConferencePaper) return ((ConferencePaper)publication).ToDTO();

            throw new ArgumentException("Wrong type of argument dto.");
        }

        private static BookDTO ToDTO(this Book book)
        {
            var dto = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Discriminator = "Book",
                ISBN = book.ISBN,
                Publisher = book.Publisher,
            };
            return dto;
        }

        private static PatentDTO ToDTO(this Patent patent)
        {
            var dto = new PatentDTO
            {
                Id = patent.Id,
                Title = patent.Title,
                PublicationDate = patent.PublicationDate,
                Discriminator = "Patent",

            };
            return dto;
        }

        private static ThesisDTO ToDTO(this Thesis thesis)
        {
            var dto = new ThesisDTO
            {
                Id = thesis.Id,
                Title = thesis.Title,
                PublicationDate = thesis.PublicationDate,
                Discriminator = "Thesis",

            };
            return dto;
        }

        private static DatasetDTO ToDTO(this Dataset dataset)
        {
            var dto = new DatasetDTO
            {
                Id = dataset.Id,
                Title = dataset.Title,
                PublicationDate = dataset.PublicationDate,
                Discriminator = "Dataset",

            };
            return dto;
        }

        private static ArticleDTO ToDTO(this Article article)
        {
            var dto = new ArticleDTO
            {
                Id = article.Id,
                Title = article.Title,
                PublicationDate = article.PublicationDate,
                Discriminator = "Article",
                PageTo = article.PageTo,
                PageFrom = article.PageFrom,
                JournalEditionId = article.Journal.Id,
            };
            return dto;
        }

        private static TechnicalReportDTO ToDTO(this TechnicalReport technicalReport)
        {
            var dto = new TechnicalReportDTO
            {
                Id = technicalReport.Id,
                Title = technicalReport.Title,
                PublicationDate = technicalReport.PublicationDate,
                Discriminator = "TechnicalReport",

            };
            return dto;
        }

        private static ConferencePaperDTO ToDTO(this ConferencePaper donferencePaper)
        {
            var dto = new ConferencePaperDTO
            {
                Id = donferencePaper.Id,
                Title = donferencePaper.Title,
                PublicationDate = donferencePaper.PublicationDate,
                Discriminator = "ConferencePaper",

            };
            return dto;
        }

        #endregion Convert to specific DTO

        #region Convert to specific entity

        public static PublicationBase ToEntity(this PublicationBaseDTO dto)
        {
            if (dto == null) throw new ArgumentNullException("dto");

            if (dto is BookDTO) return ((BookDTO)dto).CreateEntity();
            if (dto is PatentDTO) return ((PatentDTO)dto).CreateEntity();
            if (dto is ThesisDTO) return ((ThesisDTO)dto).CreateEntity();
            if (dto is DatasetDTO) return ((DatasetDTO)dto).CreateEntity();
            if (dto is ArticleDTO) return ((ArticleDTO)dto).CreateEntity();
            if (dto is TechnicalReportDTO) return ((TechnicalReportDTO)dto).CreateEntity();
            if (dto is ConferencePaperDTO) return ((ConferencePaperDTO)dto).CreateEntity();

            throw new ArgumentException("Wrong type of argument dto.");
        }

        private static Book CreateEntity(this BookDTO dto)
        {
            var book = new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
                ISBN = dto.ISBN,
                Publisher  = dto.Publisher,
            };
            return book;
        }

        private static Patent CreateEntity(this PatentDTO dto)
        {
            var patent = new Patent
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
            };

            return patent;
        }

        private static Thesis CreateEntity(this ThesisDTO dto)
        {
            var thesis = new Thesis
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
            };

            return thesis;
        }

        private static Dataset CreateEntity(this DatasetDTO dto)
        {
            var dataset = new Dataset
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
            };

            return dataset;
        }

        private static Article CreateEntity(this ArticleDTO dto)
        {
            var article = new Article
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
                PageTo = dto.PageTo,
                PageFrom = dto.PageFrom,
            };

            return article;
        }

        private static TechnicalReport CreateEntity(this TechnicalReportDTO dto)
        {
            var technicalReport = new TechnicalReport
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
            };

            return technicalReport;
        }

        private static ConferencePaper CreateEntity(this ConferencePaperDTO dto)
        {
            var conferencePaper = new ConferencePaper
            {
                Id = dto.Id,
                Title = dto.Title,
                PublicationDate = dto.PublicationDate,
            };

            return conferencePaper;
        }

        #endregion Convert to specific entity
    }
}