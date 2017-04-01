## Project Description
This is project is not really about how to use async await in ASP.NET and not about the performance testing on await. This is more about whether we should use async for CPU bound in ASP.NET other than I/O bound.

There have been many talks and articles even from some well known Microsoft guys on best practices in async programming. One of the advices is to not creating new thread for small tasks unless that the tasks are quite heavy. Creating new thread might slow down the server, instead it is best to not creating any new thread to allow server to serve more requests. 

However, there are also people think that by creating new thread, the server can serve the clients much faster. So this project is trying to simulate multiple incoming requests to one server to see whether it is true or not what those people think. 

#### PLEASE NOTE: 
* This might not be a good way to test.
* I might not be able to update this project in the future. 

#### Setup
* 2 CPUs @1.9GHZ
* A sample text file, named {"data_2012"}, which size is about 12.9MB. Unfortunately, data_2012, is not included in the source code as I have not ask for the owner permission yet. But you can use any big log files.
* The sample text file should not contain following keyword "cip-cip" in order to allow program to look through the entire file. Therefore the server will always return "Record not found"

#### How to run
* Server
** Server can be run in IIS Express
* Client
** client {"[number_of_requests]"} {"[url]"}
** client 100 "http://localhost/SearchNoThread.ashx"
** client 100 "http://localhost/SearchNewThread.ashx"


#### Results

```
SearchNoThread: 
001 requests: 00:00:00:0370
001 requests: 00:00:00:0313
001 requests: 00:00:00:0408
001 requests: 00:00:00:0332
001 requests: 00:00:00:0379
001 requests: 00:00:00:0336
010 requests: 00:00:01:0854
010 requests: 00:00:01:0628
010 requests: 00:00:01:0817
010 requests: 00:00:01:0697
010 requests: 00:00:01:0898
010 requests: 00:00:01:0813
050 requests: 00:00:10:0769
050 requests: 00:00:10:0218
050 requests: 00:00:09:0306
050 requests: 00:00:07:0879
050 requests: 00:00:07:0826
050 requests: 00:00:08:0165
100 requests: 00:00:22:0226
100 requests: 00:00:21:0007
100 requests: 00:00:17:0336
100 requests: 00:00:14:0969
100 requests: 00:00:17:0433
100 requests: 00:00:15:0258
200 requests: 00:00:29:0852
200 requests: 00:00:30:0470
200 requests: 00:00:30:0267
200 requests: 00:00:29:0931
200 requests: 00:00:29:0741
200 requests: 00:00:30:0416
500 requests: 00:01:34:0054
500 requests: 00:01:17:0041
500 requests: 00:01:24:0795
500 requests: 00:01:28:0907
500 requests: 00:01:38:0267
500 requests: 00:01:28:0183
300 requests: 00:00:53:0050
400 requests: 00:01:10:0208
400 requests: 00:01:11:0865
400 requests: 00:01:13:0238
400 requests: 00:01:19:0348
400 requests: 00:01:00:0958
600 requests: 00:01:40:0142 (time out)


SearchNewThread:
001 requests: 00:00:00:0389
001 requests: 00:00:00:0251
001 requests: 00:00:00:0332
001 requests: 00:00:00:0282
001 requests: 00:00:00:0303
001 requests: 00:00:00:0270
010 requests: 00:00:01:0761
010 requests: 00:00:01:0674
010 requests: 00:00:01:0640
010 requests: 00:00:01:0807
010 requests: 00:00:01:0846
010 requests: 00:00:01:0606
050 requests: 00:00:10:0356
050 requests: 00:00:10:0443
050 requests: 00:00:09:0585
050 requests: 00:00:09:0282
050 requests: 00:00:09:0891
050 requests: 00:00:10:0402
100 requests: 00:00:21:0247
100 requests: 00:00:19:0339
100 requests: 00:00:23:0656
100 requests: 00:00:30:0307
100 requests: 00:00:22:0601
100 requests: 00:00:26:0706
200 requests: 00:00:37:0366
200 requests: 00:00:35:0976
200 requests: 00:00:37:0191
200 requests: 00:00:36:0524
200 requests: 00:00:38:0550
200 requests: 00:00:42:0233
300 requests: 00:01:06:0621
300 requests: 00:00:54:0953
400 requests: 00:01:18:0203
400 requests: 00:01:16:0031
400 requests: 00:01:21:0996
450 requests: 00:01:40:0090 (time out)
450 requests: 00:01:35:0624
450 requests: 00:01:40:0434 (time out)

```

I did not bother to include 500 requests in SearchNewThread because with 450 requests, the server cannot handle all of them. Please note: default timeout value for HttpClient is 100 seconds.

One key concept to note here is 1 CPU can only execute one thread at a time only. There will be always a cost on having many threads i.e. the context-switching. So, anyone who wants to create new thread for CPU bound, please re-think again. There are very rare situations where you want to create new thread for CPU bound. However, please do use async for any I/O bound so that the CPU won't be blocked while waiting for the response.

Quite useful article by [Thomas Marquardt on Performing Asynchronous Work, or Tasks, in ASP.NET Applications](http://blogs.msdn.com/b/tmarq/archive/2010/04/14/performing-asynchronous-work-or-tasks-in-asp-net-applications.aspx). Even though it is an old article, the concept is still remained until today. 
