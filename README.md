# Win2dTest
Testing Win2D for UWP (Lumia 950 XL)

After the initial test of Win2D On Lumia 950 XL, i discovered that it produced glitches every now and then.

I contacted the dev team, they agreed and said they would fix it as it was a known error due to timing on the render thread
(synchronization). A fix was made, but it didnt do the trick. The issue was closed unfortunately unsolved. In my opinion,
a simple text scroller writting using the best practices should be able to run smoothly if i should ever start porting my XNA games
to Win2D. I decided to make this project, to keep testing every time updates are made to Windows 10 Mobile and Win2D.
It also allows others to contribute and comment the code even though it should be kind of straight forward.

My anchor is that a similar project was made in XNA on Lumia 800, which ran 100% smoothly. Not being able to reproduce that result
on Lumia 950 XL was a shock to me.

I just thought that Microsoft had an interest in getting as many games and apps to the Windows 10 platform. But without a API for
writing games in C# like XNA, and when C# is the most popular language in .NET, i don't see how that helps? - Adding Win2D is a brilliant
idea, as i see it as the "new" XNA, or at least for 2D games. But if it's not capable of producing smooth frame rates, its a no go for me.

If you have any ideas, suggestions or comments, please let me know. I'm sharing this, because I need your help/opinion/suggestions on this
subject. The goal is not to create a text scroller, that was just random pick. But i thought, if i can't produce a smooth text scroller,
why use hours trying to produce a smooth game with many more objects moving around :P
