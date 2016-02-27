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

Our goal is to create a single server (back-end) and have multiple clients (web, mobile, desktop, console) consuming data from the server.

**API** - Application Programming Interface - defines the functionality of various software components independent of the implementation.

In the case of web development, an API can be translated into a web service that various clients use to retrieve and update data.

Let's consider a modern application which includes a web application and several mobile apps for iOS, Android, Windows Phone and applications for Windows, Ubuntu and OS X.

![enter image description here](https://wuyuiq.bn1301.livefilestore.com/y3mTl9mYvCUNOjZFF0RZem7ChIfDbL4NQiKXKcEgOy1LSlCRoc6eMMBi_9ddPJmlF9s-lQrllha7vUsGsj1h1gpzOTkcShSA0R91j9n1eiJWdQhSFu3ZkZ3ZNSuF8IiOJSK_I7GAiGqz3zbNdzhEKSOunGiaR7DUjzP6dye0SeLxZA?width=1024&height=702&cropmode=none)

Why is this an example of a bad architecture?

If a requirement comes for the business logic, **all** applications must be updated.

We are going to create the following architecture: 

![enter image description here](https://jtjs5g.bn1301.livefilestore.com/y3mNpwuDK-HEjZ5DxmEs4HfGxrOfWUuYrAveC_iTO7t95XUTJaEQWD2LBnX1mir7uuOsGMuFWpDGxQ6rxwkMw8Cd6uhA6X6tEBVyqWpEXNWZrM5dEWAAZDkS8aDu-BqZICXoooCGXjANvJfhB4wE7_8dTBYYVfeYpoGtxXCMwCInmc?width=1021&height=448&cropmode=none)

Now, if anything needs to change in the business logic, we only need to apply that change once.

We are going to create a back-end that will be used with the easiest to implement clients: a web page and a console application.

The server accepts requests from the client (**no matter who the client is, the server can accept requests**) and gives back a response.

![enter image description here](https://hbwafw.bn1301.livefilestore.com/y3mANvEFZi4agT_N7fgET3wq6TP8D4nMKUn4cNDLroacGqjzk9iKSIcv38uqjcw3gzOpoR2Rz1iLmGW1uI0riU8Xd4Obrr5PBL8VpIPx_-Uu9EIF87vBETtQGzlAf-SYraxzz21EagM8e6OX6G33KUF_2SpEXPfE5aSG9cQ3AmqUgk?width=854&height=190&cropmode=none)

In Asp.Net Web Api, the component that accepts requests, computes it and generates the response is called a **controller**.


> For more information about Asp.Net Web Api, visit the [official documentation.](http://www.asp.net/web-api/overview)

Creating the social network
--------------------------------

We are considering a very rudimentary social network: each user that enters can publish a post containing his user name and a text.

First of all, let's see how to create a Web Api solution from Visual Studio. 

New Project --> Web --> ASP.NET Web Application
Then, enter the name and click OK.

![enter image description here](https://h5gnuq.bn1301.livefilestore.com/y3mEFjE8fAHeOBEK0HgKZvMZXCNohJ_6CSZP1QisPvPPwyYatU2kKrB3YKnZXfbCe-yhVAGbol1p8wJwMEX8WfoqtYEDiD17ncIeJcFStxW3UJYSWuH7zYq3p4jcLJ0G3bggAEZJEH0zW-GEBkh6H0j_KaMcyei1flp2xSaJFUQhWs?width=1911&height=1111&cropmode=none)

Next, choose the Empty template from the ASP.NET 4.6 with Web Api folders included, no Authentication and not hosted in the Cloud.

![enter image description here](https://ugvpxa.bn1301.livefilestore.com/y3mc7vxo6kSsVm1zn2Pqj0EJprkBiFOmaInGC_3nFWSG5nBXcb9AW59OLO6XxaAGlCs8_azPFU6Nzzg9hS8TfzXOFhkXkkrncS9j5VGwyGP2CRDPCIcVm698BxkuX-0lQy7nTjJkSp6DhTN1o2F7S-5j9grt6DJvl3XzV5MIXtmYEQ?width=1913&height=1131&cropmode=none)

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
The class also contains a constructor for initializing the values of the properties and a default constructor.

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

![enter image description here](https://vxr2qg.bn1301.livefilestore.com/y3mZ4-287E8Gms-7UNmceXoVPLgCTckZexDy-KsQd-HPG5GJEyF1N29oYuff23H8dovbVJp7j19q77rAMMCuMq5a9QgFRtdk9yFswThDzvYhqyW3TVfxl0HJTb5MnN7_S7cBud5A-9tw0lsH2zed7O78o8BvAT_5p1v_pIwoDbsw4E?width=1268&height=761&cropmode=none)

At this point, a web browser instance should start with the `URL`: `http://localhost:port_number`

If we want to see if our app works, we should navigate to the following `URL`: `/api/Posts/GetPosts`

> Note that the `URL` we have to access is composed of the controller name and the name of the method.
> In general, you can access a specific method on a controller by navigating to the following `URL`: `/api/ControllerName/MethodName`.

After navigating to the `URL` above, we can see the raw data from the server:

![enter image description here](https://gepija.bn1301.livefilestore.com/y3m0En5IZPxQTlgPbU4MLpoAdYre6GJmCLqrZW8grh3AuH_zx6ttSJU-B_TMgbHzapP1BMI_ZdOhXG83m3MSZA0juDS_eDuVc9pMQONHoWd0Sa_wnp--6L574sRrDTgk6P-fXa0CBQUKivmCql799TJ40lLNM-RG8T54MYYw9eiFU4?width=1251&height=251&cropmode=none)

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


We created an unordered list (`<ul>`) in which to display the messages.
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

If we want to see our new post, we have to refresh the page, which make the user experience really, really bad.


Adding real-time communication
---------------------------------------

If we want the user experience improved, we must add real-time communication. This means that the client does not have to refresh the browser in order to receive data.

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

Right now, whenever a user publishes a new post, all clients will be updated instantly.
