Microsoft Monday - Introduction to Web Services
==============


Introduction
--------------

> Today, we live in a **mobile** first, **cloud** first world.

So, we are surrounded by **mobile applications** and a growing number of **Internet of Things** applications,  **sensors**, **wearables** and **smart devices**.

If we talk about the mobile applications, the following questions arise: 

 - where does the data come from? 
 
 - how do we handle having our app on multiple platforms?
 
 - how do we handle real-time communication between clients?


If we talk about IoT applications, the questions are: 

- where does the data from our sensors go?

- how can we take the best decisions based on the data we acquired?

If we talk about larger applications, we could ask: 

- how can we manage an unexpected spike in the number of users of our service/mobile app?


Introduction to Web Technologies
----------------------------------------

How do we start if we want to create a web application, then add an app that has the same functionality (and have the app on multiple platforms) ?

Our goal is to create a **single server** (back-end) and have **multiple clients** (web, mobile, desktop, console) consuming data from the server.

**API** - Application Programming Interface - defines the functionality of various software components independent of the implementation.

In the case of web development, an API can be translated into a web service that various clients use to retrieve and update data.

Let's consider a modern application which includes a web application and several mobile apps for iOS, Android, Windows Phone and applications for Windows, Ubuntu and OS X.

[![01](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/01.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/01.jpg)

Why is this an example of a bad architecture?

If a requirement comes for the business logic, **all** applications must be updated.

We are going to create the following architecture: 

[![02](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/02.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/02.jpg)

Now, if anything needs to change in the business logic, we only need to apply that change once.

We are going to create a back-end that will be used with the easiest to implement clients: a web page and a console application.

The server accepts requests from the client (**no matter who the client is, the server can accept requests**) and gives back a response.

[![03](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/03.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/03.jpg)

In Asp.Net Web Api, the component that **accepts requests**, computes it and **generates the response** is called a **controller**.


> For more information about Asp.Net Web Api, visit the [official documentation.](http://www.asp.net/web-api/overview)

Creating the social network
--------------------------------

We are considering a very rudimentary social network: each user that enters can publish a post containing his user name and a text.

First of all, let's see how to create a Web Api solution from Visual Studio. 

New Project --> Web --> ASP.NET Web Application
Then, enter the name and click OK.

[![04](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/04.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/04.jpg)

Next, choose the Empty template from the ASP.NET 4.6 with Web Api folders included, no Authentication and not hosted in the Cloud.

[![05](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/01.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/05.jpg)

Now we have a basic structure for implementing the web service.

As we said this was going to be a very rudimentary social network where users can publish posts consisting of the user name and some text.

So, in our `Models` folder, let's add a class with two properties: 

    public class Post
    {
        public string UserName { get; set; }
        public string Text { get; set; }

        public Post(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }
        public Post()
        {
        }
    }
The class also contains a **constructor** for initializing the values of the properties and a **default constructor**.

> Note: following the best practices, the models should be placed in the `Models` folder and controllers in the `Controllers` folder.

Now in the `Controllers` folder, let's add a controller and call it `PostsController`.

As we said earlier, this class will be responsible for accepting requests and computing responses.

A controller is simply a class that inherits the `ApiController` class from `.NET`.   

> More information about `ApiController` is [available here.](https://msdn.microsoft.com/en-us/library/system.web.http.apicontroller%28v=vs.118%29.aspx)

Let's see how a first very basic version of our controller looks like:

        public class PostsController : ApiController
    {
        public static List<Post> Posts = new List<Post>() {
            new Post("Obi-Wan Kenobi","These are not the droids you're looking for"),
            new Post("Darth Vader","I find your lack of faith disturbing")
            };

        [HttpGet]
        public List<Post> GetPosts()
        {
            return Posts;
        }
    }

As we can see, we have a list of `Post` objects called `Posts` that I already populated with two posts.

> Note: The `Post` list is **static** because a new controller is created with each new request. 

We also have a public method called `GetPosts` that returns the entire list.

At this point, we can see if our application can accept requests. Start the application by pressing the `Start` button or by pressing `F5`.

[![06](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/01.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/06.jpg)

A web browser instance should start with the `URL`: `http://localhost:port_number`

If we want to see if our app works, we should navigate to the following `URL`: `/api/Posts/GetPosts`

> Note that the `URL` we have to access is composed of the controller name and the name of the method.
> In general, you can access a specific method on a controller by navigating to the following `URL`: `/api/ControllerName/MethodName`.

After navigating to the `URL` above, we can see the raw data from the server:

[![07](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/01.jpg?raw=true)](https://github.com/microsoft-dx/ms-monday-social-network/blob/master/images/07.jpg)

> Note that in order to see the JSON formatted this way, I used a [Google Chrome Extension called JSON Formatter.](https://chrome.google.com/webstore/detail/json-formatter/bcjindcccaagfpapjjmafapmmgkkhgoa?hl=en)

We can also create a method that adds a `Post` in our list. What this method does is take a `Post` object as argument and add it to the list.

        [HttpPost]
        public void AddPost(Post post)
        {
            Posts.Add(post);
        }

> Note that the `[HttpPost]` attribute has nothing to do with the name of our model, but with the type of `HTTP` method. For more information, please [visit this resource.](https://en.wikipedia.org/wiki/Hypertext_Transfer_Protocol#Request_methods)

At this point, we can create an `HTML` page that displays and adds posts.

For this step, we are only displaying the posts:

 

    <!DOCTYPE html>
    <html>
     <head>
        <title></title>
    	<meta charset="utf-8" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.2.0.min.js"></script>
    
      </head>
    <body>

     <ul id="postsList"></ul>
       
    <script type="text/javascript">
        $.ajax({
            url: '/api/Posts/GetPosts',
            method: 'GET',
            dataType: 'json',
            success: addPostsList
        });

        function addPostsList(posts) {
            $.each(posts, function (index) {
                var post = posts[index];
                addPost(post);
            });
        }

        function addPost(post) {
            $("#postsList").append(
                    '<li><b>' + post.UserName + '</b><br>' + post.Text + '</li><br>'
                 );
        }
      </script>
     </body>
    </html>


We created an **unordered list** (`<ul>`) in which to display the messages.
Then, we used `jQuery` to make a request to the server and call the method `GetPosts`. If the request is successful, then call the `addPost` method.

> For a complete documentation for the `ajax` method, [see this resource.](http://api.jquery.com/jquery.ajax/)

Now, we are ready to add the code that publishes posts.

We need two text inputs for the user name and the post and a button.


    <!DOCTYPE html>
    <html>
     <head>
        <title></title>
    	<meta charset="utf-8" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.2.0.min.js"></script>
    
      </head>
    <body>
	<input id="userNameInput" type="text" placeholder="Enter your user name..." />
    <input id="textInput" type="text" placeholder="Enter your status..." />

    <button id="publishPostButton">Publish post!</button>
     <ul id="postsList"></ul>
       
    <script type="text/javascript">
        $.ajax({
            url: '/api/Posts/GetPosts',
            method: 'GET',
            dataType: 'json',
            success: addPostsList
        });

        function addPostsList(posts) {
            $.each(posts, function (index) {
                var post = posts[index];
                addPost(post);
            });
        }

        function addPost(post) {
            $("#postsList").append(
                    '<li><b>' + post.UserName + '</b><br>' + post.Text + '</li><br>'
                 );
        }
        $("#publishPostButton").click(function () {

            var post = {
                UserName: $("#userNameInput").val() || "Guest",
                Text: $("#textInput").val()
            };
            $.ajax({
                url: '/api/Posts/AddPost',
                method: 'POST',
                dataType: 'json',
                data: post
            });

        });
      </script>
     </body>
    </html>

If we want to see our new post, we have to **refresh the page**, which make the user experience really, really bad.


Adding real-time communication
---------------------------------------

If we want the user experience improved, we must add **real-time communication**. This means that **the client does not have to refresh the browser** in order to receive data.

To achieve this, we are going to use a library called `SignalR` that really simplifies the communication.

We add the `SignalR`package using NuGet Package Manager.  (Right click the project --> Manage NuGet Packages, then search for SignalR).

> More information about SignalR at the [official documentation.](http://www.asp.net/signalr)

In order to connect clients, we need a class called a `Hub`. This class will manage the connection of clients, and will help us send real-time messages.

> A hub is just a class that inherits the `Hub` class.

Before we start, we must let the API know that we are going to use SignalR. We do this by adding a `Startup` class .

        public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }


> In order to follow the convention, we add a new folder called `Hubs` where we will put our hub classes.

In our `Hubs` folder we create a new hub called `PostHub`, and leave it empty.

        public class PostHub : Hub
        {
        }


In order to update all clients, we modify the `AddPost` method: 

            [HttpPost]
        public void AddPost(Post post)
        {
            Posts.Add(post);
            GlobalHost.ConnectionManager.
                GetHubContext<PostHub>().Clients.All.publishPost(post);
        }


Now, we connect the clients to the hub and define the `publishPost` method: 

    <!DOCTYPE html>
    <html>
    <head>
        <title></title>
    	<meta charset="utf-8" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.2.0.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.0.min.js"></script>
    <script src="/signalr/hubs"></script>
    </head>
    <body>
    
    <input id="userNameInput" type="text" placeholder="Enter your user name..." />
    <input id="textInput" type="text" placeholder="Enter your status..." />

    <button id="publishPostButton">Publish post!</button>

    <ul id="postsList"></ul>
    
    <script type="text/javascript">
        $.ajax({
            url: '/api/Posts/GetPosts',
            method: 'GET',
            dataType: 'JSON',
            success: addPostsList
        });

        function addPostsList(posts) {
            $.each(posts, function (index) {
                var post = posts[index];
                addPost(post);
            });
        }

        function addPost(post) {
            $("#postsList").append(
                    '<li><b>' + post.UserName + '</b><br>' + post.Text + '</li><br>'
                 );
        }

        var hub = $.connection.postHub;

        hub.client.publishPost = addPost;

        $("#publishPostButton").click(function () {

            var post = {
                UserName: $("#userNameInput").val() || "Guest",
                Text: $("#textInput").val()
            };
            $.ajax({
                url: '/api/Posts/AddPost',
                method: 'POST',
                dataType: 'json',
                data: post
            });

        });

        $.connection.hub.start();
    </script>
     </body>
    </html>

Right now, **whenever a user publishes a new post, all clients will be updated instantly.**


Creating the console client
--------------------------------

As we said earlier, this API can be used with any client that supports `HTTP` communication.

Since is the easiest to implement, we will consider creating a console application that displays the posts that are published, also using real-time communication.

Basically, we do the same `HTTP`operation, but this time in a `.NET` client.

                using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26315");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Posts/GetPosts");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var posts = JsonConvert.DeserializeObject<List<Post>>(data);
                    foreach (var post in posts)
                        PrintPost(post);
                }
            }

> Note: If the console client doesn't work, you might want to check the `localhost` instance that the web server is running and modify it in `Program.cs`

            static void PrintPost(Post post)
        {
            System.Console.WriteLine(
            "{0}: {1}", post.UserName, post.Text);
        }

> For more information about consuming data from Web Api in a .NET client, [check the official documentation.](http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client)

In order to add the real-time functionality, we use the following: 

            var hubConnection = new HubConnection("http://localhost:26315");
            var hub = hubConnection.CreateHubProxy("PostHub");

            hub.On<Post>("publishPost", (post) => PrintPost(post) );
            hubConnection.Start().Wait();

Again, we do the same thing as in JavaScript, this time in a .NET client.

> For more information about using SignalR with a .NET client, [check the official documentation.](http://www.asp.net/signalr/overview/guide-to-the-api/hubs-api-guide-net-client)

Next steps
-------------

Right now, every time we stop the debugger, the data we insert in lost, because we keep our posts in a `List` in memory.

The first most obvious step would be to **add database support** to our application.

In this case, we would keep the posts in a `List` in memory anymore, but in a `SQL` database.

> For information about managing `SQL` databases from web applications, [check this resource.](https://azure.microsoft.com/en-us/documentation/articles/web-sites-dotnet-rest-service-aspnet-api-sql-database/)
> This resource uses an `SQL` database host in [Azure (Microsoft Azure SQL Service).](https://azure.microsoft.com/en-us/documentation/services/sql-database/)

We should also add **authentication and authorization** to our application.

> For samples and documentation about authentication and authorization, [see this resource.](http://www.asp.net/web-api/overview/security)

Another step is to publish our entire application in the cloud. This would give us the ability to **scale up and down on demand**.


Furthermore, we can start creating mobile applications that consume data from the server we just created.


> [Resources for developing iOS applications using C# code here (Xamarin).](https://developer.xamarin.com/guides/ios/getting_started/)
>[ Resources for developing Android applications using C# code here (Xamarin).](https://developer.xamarin.com/guides/android/getting_started/)
> 
> The advantage of using Xamarin for mobile applications is that you create applications for all major mobile platforms using the same code for the logic of the application.

Next, we can deliver real-time **push notifications** to these mobile applications using Azure Push Notification Service.

> [Documentation for delivering push notifications for most mobile and desktop platforms using Microsoft Azure.](https://azure.microsoft.com/en-us/documentation/services/notification-hubs/)

Conclusion
-------------

We created a web service that is capable of delivering data to most clients available and communicates in real-time with the clients.

Moreover, we can extend the project and add mobile applications with push notifications, all of these with a minimum of code.

We are able to serve all client capable of `HTTP` communication writing a single web service for the back-end and reusing most of the code for the mobile applications.
