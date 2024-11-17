This project has been created out a simple sense of curiosity.

# Doctor Who Ratings

This site allows ratings for the Doctor Who TV series to be shown, compared, and contrasted. A wide range of charts, graphs, and analysis, will be added over time.

View live here: http://www.russellmason.com/doctor-who-ratings  
(please be patient as the site will probably need a few moments to warm up)

# Getting Started

1.  Clone the repository

        git clone [github path]

2.  Navigate into the web client project folder

        cd doctor-who-ratings/DoctorWhoRatings.WebClient

3.  Run the app<br>
    Using the dotnet CLI:<br>

        dotnet run
		
4.  Open a browser and navigate to http://localhost:5004

**Notes:**

Two lines of code have been explicitly added to make local development and deployment to the live website easier: 

1. Only HTTP is supported. You'll need to re-enable HTTPS redirection if you need it. 
This can be done by uncommenting the following line in the Program.cs file:
```
// app.UseHttpsRedirection();
```
2. A different application root is used. This is selected based on local execution via the App.razor file.
```
@{
    var baseHref = Env.IsDevelopment() ? "/" : "/doctor-who-ratings/";
}
```
