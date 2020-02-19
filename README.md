# Computer-science-year-2-project

# 1. folder architecture
1 - design structure/architecture of the folder for files ()
    ---info---
     - Libraries
     - custom files

    ---files---
     - base html for page (HTML)
     - html for extension (HTML)
     - gui for html pages using the p5 libraries (js)
     - node module (js)
     - tree module (js)
     - manifest file for chrome extension (json)
     - background script for chrom extension (js)
	 
	---setup---
	 - index html
	 - pop up html
	 - manifest json file

# 2. tab data
2 - pull tab data.
     ---info---
      - URL (string)
      - title (string)
      - faviconUrl (string)
      - sessionId (string)
      - active (boolean)

# 3. node design
3 - design node class for tree
    ---info---
    varibles {
	
        - URL to website
        - favicon from tab (if one)
        - date and time of arrival
        - date and time of departure
        - number of time visited on that tab
        - parent node
        - child array of nodes
        - title
        - weather active or not 
        - session ID
        
        --gui--
        - width
        - height
        - line lengths
		- label
		- text for text box
		- favicon image
		- title label
        
        
     }

     methods {
        - Get the current date and time (getdate)
        - Add a child to a node (addChild)
        - Remove a child from parent (removeChild)
        - view node, return info like childrent parent and it's URL (veiwNode)
        - validation used for searchs (nodeValidation)
        - Date comparision (CompareDate)
		- Make new gui node for the gui
		- Algorithm for sapcing 
     }

# 4. tree design
4 - design tree class
    ---info---
    variables{
        - root
		- current node
		- array of pre-existing nodes
		- zipper
    }
	
	methods{
		- Add new node
		- Change current node
	}
	
# 5. design zipper
5 - design zipper class
	---info---
	- still got to research
	
	
	
	
	
	
	
	
	
	
	
	
	
	