Touch screen terminal (kiosk) embedded application for movie ticket selling. Semester project.

Made with WPF which is touchscreen ready out of the box. Custom controls with simple design and large hitzones implemented.

Data is stored in an Microsoft SQL Server database. 

MVVM pattern intended but it's MVC in fact because I store links to `View` to implicitly call it from `ViewModel`.

# Things I was proud of when I just finished the project

* Seat selection screen
  * It utilizes `Viewbox` and it's ability to scale inner controls (to fit cinema rooms of different sizes)
  * It generates controls dynamically
  * Every seat control is a styled checkbox
* Project structure
* Quick start wizard (because my teacher didn't manage to prepare database and compile the project)

# Screenshots

### Quick start wizard

![image](https://user-images.githubusercontent.com/13202642/29816845-72c0232a-8cbe-11e7-997a-da18534891a3.png)

![image](https://user-images.githubusercontent.com/13202642/29816864-8a71ba2e-8cbe-11e7-9685-0f073b423800.png)

![image](https://user-images.githubusercontent.com/13202642/29816889-9dba116c-8cbe-11e7-9e29-b43d81df1925.png)

### Admin app

![image](https://user-images.githubusercontent.com/13202642/29816909-b212818a-8cbe-11e7-8b4a-c5f06967c498.png)

### Kiosk app

It launches windowed only in debug mode. I have too large screen to make screenshots in fullscreen mode and I'm too lazy to mess around with the resolution. `¯\_(ツ)_/¯`

As always I have put a lot of effort into UI and UX and hope it was worth it.

![image](https://user-images.githubusercontent.com/13202642/29744193-ae5a8e60-8aa8-11e7-9082-2c717793ce4c.png)

![image](https://user-images.githubusercontent.com/13202642/29744202-dd2b66e2-8aa8-11e7-843e-e6cc16338ca8.png)

![image](https://user-images.githubusercontent.com/13202642/29744194-b22741fa-8aa8-11e7-88a0-fc8d3029268b.png)

![image](https://user-images.githubusercontent.com/13202642/29744195-b83c1c50-8aa8-11e7-8f31-79b19c8c2ede.png)

![image](https://user-images.githubusercontent.com/13202642/29744198-c2f21ff0-8aa8-11e7-957b-34df1fd439b8.png)

![image](https://user-images.githubusercontent.com/13202642/29744199-c71aca46-8aa8-11e7-86c1-edc53988a198.png)

