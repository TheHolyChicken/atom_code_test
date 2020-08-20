== Atom Coding Challenge Readme ==

This implementation requires the "System.Drawing.Common" Version="4.7.0" package.

Inside the 'Program.cs' file you must:

* define 'cache_folder' (line 64) as the folder where you'd like cached images to be saved
* edit 'createTestImageRequests()' (line 10) to adjust the test sequence of images the program will load

The application will provide the requested images, preferring to edit only if necessary and only if there is no cached copy already created.


== Possible Improvements ==

*	The program is not currently asyncronous
*	The cache is not currently persistent, so will reset when the program resets
*	There are no cache limits or cleanup, so eventually you could fill up the hard drive with
	cached images. Ideally the cache would keep itself bound to a certain size, preferring to
	delete oldest or least frequently accessed images.
*	Some areas where exception handling could likely be improved


- Colin Pearce
