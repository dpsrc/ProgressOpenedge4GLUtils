
When I started working with **Progress Openedge 4GL** (unexpectedly for me, worth to mention) back in 2005, the built-in possibility of seeing the DB structure was not enough for the visual representation of the tables, fields, their descriptions, and other attributes (like format, nullability, default value, indices, etc.). Moreover, on the boxes without **Progress** developer license it was too tricky to access even the built-in explorer.

Thus I implemented the easy, but effective, graphical **Progress** database structure explorer with the trafitional three explorer-like panels. **Progress 4GL** can export the database structure into a .df file. This DB structure explorer takes the .df file and explores the DB structure described in that .df file.

