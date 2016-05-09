
/* admin */

GRANT EXECUTE ON dbo.BrowseMovies TO greenbird_admin
GO
GRANT EXECUTE ON dbo.MovieDetails TO greenbird_admin
GO
GRANT EXECUTE ON dbo.ListAgeLimits TO greenbird_admin
GO
GRANT EXECUTE ON dbo.BrowseGenres TO greenbird_admin
GO
GRANT EXECUTE ON dbo.CreateMovie TO greenbird_admin
GO
GRANT EXECUTE ON dbo.UpdateMovie TO greenbird_admin
GO
GRANT EXECUTE ON dbo.DeleteMovie TO greenbird_admin
GO
GRANT EXECUTE ON dbo.CreateGenre TO greenbird_admin
GO
GRANT EXECUTE ON dbo.UpdateGenre TO greenbird_admin
GO
GRANT EXECUTE ON dbo.DeleteGenre TO greenbird_admin
GO
GRANT EXECUTE ON dbo.CreateShowtime TO greenbird_admin
GO
GRANT EXECUTE ON dbo.UpdateShowtime TO greenbird_admin
GO
GRANT EXECUTE ON dbo.DeleteShowtime TO greenbird_admin
GO
GRANT EXECUTE ON dbo.BrowseShowtimes TO greenbird_admin
GO
GRANT EXECUTE ON dbo.BrowseAuditoriums TO greenbird_admin
GO
GRANT EXECUTE ON dbo.GetLogo TO greenbird_admin
GO
GRANT EXECUTE ON dbo.SetLogo TO greenbird_admin
GO
GRANT EXECUTE ON dbo.CreateAuditorium TO greenbird_admin
GO
GRANT EXECUTE ON dbo.DeleteTicket TO greenbird_admin
GO
GRANT EXECUTE ON dbo.UpdateAuditorium TO greenbird_admin
GO
GRANT EXECUTE ON dbo.DeleteAuditorium TO greenbird_admin
GO
GRANT EXECUTE ON dbo.BrowseTickets TO greenbird_admin
GO
GRANT EXECUTE ON dbo.BrowseLogs TO greenbird_admin
GO
GRANT EXEC ON TYPE::dbo.IdList TO greenbird_admin
GO
GRANT EXEC ON TYPE::dbo.SeatList TO greenbird_admin
GO

/* user */

GRANT EXECUTE ON dbo.BrowseMovies TO greenbird_user
GO
GRANT EXECUTE ON dbo.MovieDetails TO greenbird_user
GO
GRANT EXECUTE ON dbo.BrowseGenres TO greenbird_user
GO
GRANT EXECUTE ON dbo.GetLogo TO greenbird_user
GO
GRANT EXECUTE ON dbo.BrowsePendingShowtimes TO greenbird_user
GO
GRANT EXECUTE ON dbo.RegisterTickets TO greenbird_user
GO
GRANT EXECUTE ON dbo.GetOccupiedSeats TO greenbird_user
GO
GRANT EXEC ON TYPE::dbo.SeatList TO greenbird_user
GO

/* superadmin */

GRANT ALTER ANY USER TO greenbird_superadmin
GO
GRANT EXECUTE ON dbo.BrowseUsers TO greenbird_superadmin
GO
GRANT EXECUTE ON dbo.CreateUser TO greenbird_superadmin
GO
GRANT EXECUTE ON dbo.DeleteUser TO greenbird_superadmin
GO