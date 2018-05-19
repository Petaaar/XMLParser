# XMLParser
### by Petar Angelov - [Petaaar](https://github.com/Petaaar/)

### The project

This is a testing project, I'm trying to create a __fully - automated__ parser from XML-formated file with extension __*.sashs*__ to
working C# code.

An example picture of the __*Example.sashs*__ file:  
![example](https://user-images.githubusercontent.com/26832131/40060452-50abad82-585f-11e8-94cb-14930774a870.png)  

### But how to use it?  

When the program starts you will be asked  for a path to a __*.sashs*__  XML-formated file like __Example.sashs__. The program will check if your file is well-formated and is valid XML file with extension __*.sashs*__.   
After it's done, the program will return a __C#__ class, generated via the __*.sashs*__ file.  

### The nodes and how to use them:  
Every node has it's own usage, syntax and arguments(attributes).  
Syntax : __<node argument1="" argument2="">name</node>__. Every node requires some parameters, one or more of them could be optional as well.  

__*nameSpace*__ -> The __starting__ program node. Everything out of it is ignored.
###### Parameters:  
name="" -> __*Required*__ __non-empty__ parameter. If is empty or does not exists the program will throw an __EXCEPTION__.   

__*dependencies*__ -> Represents a list of all __assembly refferences__ needed. Has no parameters.  

__*ref*__ -> Represents an __assembly refference__ needed for the class. __Must be contained__ in the __*dependencies*__ tag.  
###### Parameters:   
using="" ->  __*Required*__ __non-empty__ parameter. If is empty or does not exists the program __won't__ import the specified __assembly refference__.  

__*class*__ -> Represents the initialization of a __class__.  
###### Parameters:    
protectionLevel="" -> __*Optional*__ parameter. The program will run with or without it.  
type="" -> __*Optional*__ parameter. The program will run with or without it.  
name="" ->__*Required*__ __non-empty__ parameter. If is empty or does not exists the program will throw an __EXCEPTION__.  

> Every parameter from this point forth __must be contained__ in the __*class*__ tag.  

__*privateFields*__ -> Represents a list of all __private fields__ of the class. Has no parameters.  

__*item*__ -> Represents a __private field__ of the class.  
###### Parameters:  
type="" -> __*Optional*__ paramerer. Determines if the __field__ is __static__, __readonly__, __constant__ or has no specified type.   
returnType="" -> __*Required*__ parameter. If is empty or does not exists the __field__ __*won't be created*__. You will get error comment instead.  
__The node name__ must be non-empty as well, it's field's name as well.   

__*encapsulate*__ -> Represents a boolean(true or false). If it's false the __privateFields__ won't be encapsulated. The __node name__ must be __non-empty__!  

__*publicFields*__ -> Represents a list of all __public fields__ of the class. Has no parameters.  

__*publicItem*__ -> Represents a __public field__ of the class.  
###### Parameters:    
type="" -> __*Optional*__ paramerer. Determines if the __field__ is __static__, __readonly__, __constant__ or has no specified type.  
returnType="" -> __*Required*__ parameter. If is empty or does not exists the __field__ __*won't be created*__. You will get error comment instead.  
__The node name__ must be non-empty as well, it's field's name as well.  

__*privateMethods*__ -> Represents a list of all __private methods__ of the class. Has no parameters.  

__*method*__ -> Represents a __private method__ of the class.  
###### Parameters:  
type="" -> __*Optional*__ paramerer. Determines if the __method__ is __static__, __virtual__, __abstract__ or has no specified type.  
returnType="" -> __*Required*__ parameter. If is empty or does not exists the __method__ __*won't be created*__. You will get error comment instead.  
__The node name__ must be non-empty as well, it's field's name as well.  

__*privateMethods*__ -> Represents a list of all __private methods__ of the class. Has no parameters.  

__*publicMethod*__ -> Represents a __public method__ of the class.  
###### Parameters:    
type="" -> __*Optional*__ paramerer. Determines if the __method__ is __static__, __virtual__, __abstract__ or has no specified type.  
returnType="" -> __*Required*__ parameter. If is empty or does not exists the __method__ __*won't be created*__. You will get error comment instead.   
__The node name__ must be non-empty as well, it's field's name as well.  

### The value="" argument   
This is (maybe) the most important argument of the given node. It may be optional, as well as required.

If the node is _CONST_ or __READONLY__-type node this argument is __*REQUIRED*__.  
Else the __value__ argument is optional.   

If the __value__ and the node's __RETUTN TYPE__ __*does not match*__ the field won't be created.  