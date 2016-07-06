# Open XML SDK for NET Platform Standard 3 & .net Core
a NET Platform Standard implementation of Open XML SDK 2.5 

This library(https://github.com/OfficeDev/Open-XML-SDK) was the only library that was holding back our project from supporting .net core. We didn't get any feedback from the team that created(there are more than 5 issues about it) and we couldn't wait. 
So I copied the code and i tried it out.
Guess what, it went well. What we use seems to be fine.

I had to copy some classes from core-fx repo to make it work in Utility.Xml project. Also some errors are quickly dirty fixed. The problem was with the XmlTextWriter (its internal in core-fx and this project inherit from this class).
