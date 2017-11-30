# MS-SHLLINK.Net
Microsoft Shell Link format for .Net

[Official format documentation from Microsoft](https://msdn.microsoft.com/en-us/library/dd871305.aspx)

Why?
----

I know there are some other libs that will do this, but I wanted one that was less intervening. I mean, I didn't want exceptions, I dindn't want automatic validations.

What I wanted was control:

 - a set of classes that can represent the Shell Link format, much like POCO objects

 - work with these objects via extension methods: load, save, validate and repair

 - load/save won't throw format exceptions, they will accept invalid data

 - want to validate, call the Check method

 - want to repair things before saving, call Repair method

License
-------

MIT = do whatever you wish, but don't blame me
