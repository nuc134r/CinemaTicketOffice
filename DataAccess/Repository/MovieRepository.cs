using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private List<Genre> genres;
        private List<Movie> movies;

        public List<Movie> Movies
        {
            get
            {
                movies = movies ?? LoadMovies();
                return movies;
            }
        }

        public List<Genre> Genres
        {
            get
            {
                genres = genres ?? LoadGenres();
                return genres;
            }
        }

        public void RefreshData()
        {
            movies = LoadMovies();
            genres = LoadGenres();
        }

        private List<Movie> LoadMovies()
        {
            return new List<Movie>
            {
                MovieBuilder.Create()
                    .WithTitle("Вечное сияние чистого разума")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\5492.jpg")))
                    .WithGenre(genres[3])
                    .WithGenre(genres[1])
                    .WithGenre(genres[2])
                    .WithShowtime(03.20.PM())
                    .WithAgeLimit(16)
                    .WithDuration(108)
                    .WithPlot(
                        "Наконец-то изобретена машина, которая позволяет избавиться от любых воспоминаний. Джоэль и Клементина решают выбросить друг друга из головы. Но в памяти Джоэля все еще живы самые нежные моменты их чувства. Чем меньше он помнит, тем больше любит.\n\nПонимая, что он просто обожает Клементину, Джоэль пытается найти способ, чтобы вернуть любимой память о прошлом. Пока еще не очень поздно… Он должен победить ненавистный компьютерный мозг во что бы то ни стало!")
                    .WithReleaseDate(09.03.of(2004))
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Форрест Гамп")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\448.jpg")))
                    .WithGenre(genres[2])
                    .WithGenre(genres[3])
                    .WithDuration(142)
                    .WithAgeLimit(12)
                    .WithPlot(
                        "От лица главного героя Форреста Гампа, слабоумного безобидного человека с благородным и открытым сердцем, рассказывается история его необыкновенной жизни.\n\nФантастическим образом превращается он в известного футболиста, героя войны, преуспевающего бизнесмена. Он становится миллиардером, но остается таким же бесхитростным, глупым и добрым. Форреста ждет постоянный успех во всем, а он любит девочку, с которой дружил в детстве, но взаимность приходит слишком поздно.")
                    .WithReleaseDate(23.06.of(1994))
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Кунг-фу Панда")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\103734.jpg")))
                    .WithGenre(genres[6])
                    .WithGenre(genres[0])
                    .WithGenre(genres[4])
                    .WithGenre(genres[8])
                    .WithDuration(91)
                    .WithPlot(
                        "Спасение Долины Мира и всех ее обитателей от непобедимого и безжалостного мастера Тай Лунга должно лечь на плечи Воина Дракона, Избранного среди лучших из лучших, коим становится… неуклюжий, ленивый и вечно голодный панда По.\n\nЕму предстоит долгий и трудный путь к вершинам мастерства кунг-фу бок о бок с легендарными воинами: Тигрой, Обезьяной, Богомолом, Гадюкой и Журавлем. По постигнет тайну древнего Свитка и станет Воином Дракона только в том случае, если сможет поверить в себя…")
                    .WithReleaseDate(15.05.of(2008))
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Век Адалин")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\522876.jpg")))
                    .WithGenre(genres[2])
                    .WithGenre(genres[3])
                    .WithGenre(genres[9])
                    .WithDuration(113)
                    .WithAgeLimit(16)
                    .WithPlot(
                        "По сюжету главная героиня родилась вместе с XX веком и живет на свете уже сто лет, но при этом не стареет. Несмотря на свою долгую жизнь, Адалин так и не смогла найти любимого человека. Однако наконец-то она встречает мужчину, ради которого сможет снова стать смертной и состариться вместе с ним.")
                    .WithReleaseDate(08.04.of(2015))
                    .Please()
            };
        }

        private List<Genre> LoadGenres()
        {
            return new List<Genre>
            {
                GenreBuilder.Create()
                    .WithName("боевик") // 0
                    .Please(),
                GenreBuilder.Create()
                    .WithName("фантастика") // 1
                    .Please(),
                GenreBuilder.Create()
                    .WithName("драма") // 2
                    .Please(),
                GenreBuilder.Create()
                    .WithName("мелодрама") // 3
                    .Please(),
                GenreBuilder.Create()
                    .WithName("комедия") // 4
                    .Please(),
                GenreBuilder.Create()
                    .WithName("документальный") // 5
                    .Please(),
                GenreBuilder.Create()
                    .WithName("мультфильм") // 6
                    .Please(),
                GenreBuilder.Create()
                    .WithName("приключения") // 7
                    .Please(),
                GenreBuilder.Create()
                    .WithName("семейный") // 8
                    .Please(),
                GenreBuilder.Create()
                    .WithName("фэнтези") // 9
                    .Please()
            };
        }
    }
}