CREATE PROCEDURE [dbo].[usp_GetPlaceListByCity]
	@city nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT pm.Id,CityId,pm.Name,PlaceDescription,BestTimeToVisit,Distance,Tips,ImageUrl,pm.Latitude,pm.Longitude,[Address]
	from dbo.Place pm
	JOIN dbo.City cm on pm.CityId=cm.Id
	WHERE cm.Name=@city;
END
GO