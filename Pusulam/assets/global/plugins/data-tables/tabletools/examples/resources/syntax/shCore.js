﻿/**
* Syntax Highlighter.
* http://code.google.com/p/syntaxhighlighter/
* 
* @version
* 1.6
*
* @author
* Alex Gorbatchev
* 
* @copyright
* Copyright (C) 2004-2008 Alex Gorbatchev
*
* @license
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, version 3 of the License.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
var dp = {
    sh:
	{
	    Version: '1.6',

	    Strings:
		{
		    aboutDialog: '<html><head><title>About...</title></head><body class="dp-about"><table cellspacing="0"><tr><td class="copy"><p class="title">dp.SyntaxHighlighter</div><div class="para">Version: {V}</p><p><a href="http://code.google.com/p/syntaxhighlighter/" target="_blank">http://code.google.com/p/syntaxhighlighter</a></p>&copy;2004-2008 Alex Gorbatchev.</td></tr><tr><td class="footer"><input type="button" class="close" value="OK" onClick="window.close()"/></td></tr></table></body></html>'
		},

	    ClipboardSwf: null,

	    Toolbar:
		{
		    /**
		    * Creates new toolbar for a highlighter.
		    * 
		    * @private
		    * 
		    * @param {Highlighter} highlighter    Target highlighter.
		    */
		    create: function (highlighter) {
		        var div = document.createElement('DIV');

		        div.className = 'toolbar';

		        for (var name in dp.sh.Toolbar.Commands) {
		            var cmd = dp.sh.Toolbar.Commands[name];

		            if (cmd.check != null && !cmd.check(highlighter))
		                continue;

		            div.innerHTML += '<a href="#" onclick="dp.sh.Toolbar.command(\'' + name + '\',this);return false;">' + cmd.label + '</a>';
		        }

		        return div;
		    },

		    /**
		    * Executes a command.
		    * 
		    * @private
		    * 
		    * @param {String} name     Name of the command to execute.
		    * @param {Element} sender  Sender element.
		    */
		    command: function (name, sender) {
		        var n = sender;

		        // find the highlighter			
		        while (n != null && n.className.indexOf('dp-highlighter') == -1)
		            n = n.parentNode;

		        if (n != null)
		            dp.sh.Toolbar.Commands[name].func(sender, n.highlighter);
		    },

		    /**
		    * Commands collection.
		    */
		    Commands:
			{
			    /**
			    * Command to expand the source view.
			    */
			    expandSource:
				{
				    label: '+ expand source',
				    check: function (highlighter) {
				        return highlighter.collapse;
				    },
				    func: function (sender, highlighter) {
				        sender.parentNode.removeChild(sender);
				        highlighter.div.className = highlighter.div.className.replace('collapsed', '');
				    }
				},

			    /**
			    * Command to open a new window and display the original unformatted source code inside.
			    */
			    viewSource:
				{
				    label: 'view source',
				    func: function (sender, highlighter) {
				        var code = dp.sh.Utils.fixForBlogger(highlighter.originalCode).replace(/</g, '&lt;');
				        var wnd = window.open('', '_blank', 'width=750, height=400, location=0, resizable=1, menubar=0, scrollbars=0');
				        wnd.document.write('<textarea style="width:99%;height:99%">' + code + '</textarea>');
				        wnd.document.close();
				    }
				},

			    /**
			    * Comman to copy the original source code in to the clipboard. 
			    * Uses either IE only method or Flash object if ClipboardSwf is configured.
			    */
			    copyToClipboard:
				{
				    label: 'copy to clipboard',
				    check: function () {
				        return window.clipboardData != null || dp.sh.ClipboardSwf != null;
				    },
				    func: function (sender, highlighter) {
				        var code = dp.sh.Utils.fixForBlogger(highlighter.originalCode)
							.replace(/&lt;/g, '<')
							.replace(/&gt;/g, '>')
							.replace(/&amp;/g, '&')
						;

				        if (window.clipboardData) {
				            window.clipboardData.setData('text', code);
				        }
				        else if (dp.sh.ClipboardSwf != null) {
				            var flashcopier = highlighter.flashCopier;

				            if (flashcopier === null) {
				                flashcopier = document.createElement('div');
				                highlighter.flashCopier = flashcopier;
				                highlighter.div.appendChild(flashcopier);
				            }

				            flashcopier.innerHTML = '<embed src="' + dp.sh.ClipboardSwf + '" FlashVars="clipboard=' + encodeURIComponent(code) + '" width="0" height="0" type="application/x-shockwave-flash"></embed>';
				        }

				        alert('The code is in your clipboard now');
				    }
				},

			    /**
			    * Command to print the colored source code.
			    */
			    printSource:
				{
				    label: 'print',
				    func: function (sender, highlighter) {
				        var iframe = document.createElement('IFRAME');
				        var doc = null;

				        // this hides the iframe
				        iframe.style.cssText = 'position:absolute;width:0px;height:0px;left:-500px;top:-500px;';

				        document.body.appendChild(iframe);
				        doc = iframe.contentWindow.document;

				        dp.sh.Utils.copyStyles(doc, window.document);
				        doc.write('<div class="' + highlighter.div.className.replace('collapsed', '') + ' printing">' + highlighter.div.innerHTML + '</div>');
				        doc.close();

				        iframe.contentWindow.focus();
				        iframe.contentWindow.print();

				        alert('Printing...');

				        document.body.removeChild(iframe);
				    }
				},

			    /**
			    * Command to display the about dialog window.
			    */
			    about:
				{
				    label: '?',
				    func: function (highlighter) {
				        var wnd = window.open('', '_blank', 'dialog,width=300,height=150,scrollbars=0');
				        var doc = wnd.document;

				        dp.sh.Utils.copyStyles(doc, window.document);

				        doc.write(dp.sh.Strings.aboutDialog.replace('{V}', dp.sh.Version));
				        doc.close();
				        wnd.focus();
				    }
				}
			} // end of dp.sh.Toolbar.Commands
		}, // end of dp.sh.Toolbar

	    Utils:
		{
		    parseParams: function (str) {
		        var match,
					result = {},
					regex = /([\w-]+)\s*:\s*([\w-]+)/g
					;

		        while ((match = regex.exec(str)) != null)
		            result[match[1]] = (match[2] != null) ? match[2] : true;

		        return result;
		    },

		    /**
		    * Turns all URLs in the code into A nodes.
		    * 
		    * @private
		    * @param {String} code    Source code.
		    * @return {String}        Returns code with all URLs replaced.
		    */
		    processUrls: function (code) {
		        return code.replace(dp.sh.RegexLib.url, function (m) {
		            return '<a href="' + m + '">' + m + '</a>';
		        })
		    },

		    /**
		    * Wraps each line of the string into <span/> tag with given
		    * style applied to it.
		    * 
		    * @private
		    * @param {String} str   Input string.
		    * @param {String} css   Style name to apply to the string.
		    * @return {String}      Returns input string with each line
		    *                       surrounded by <span/> tag.
		    */
		    decorateBit: function (str, css) {
		        if (str === null || str.length === 0)
		            return str;

		        str = str.replace(/</g, '&lt;');

		        // Replace two or more sequential spaces with
		        // &nbsp; leaving last space untouched.
		        str = str.replace(/ {2,}/g, function (m) {
		            var spaces = '';

		            for (var i = 0; i < m.length - 1; i++)
		                spaces += '&nbsp;';

		            return spaces + ' ';
		        });

		        if (css != null)
		        // make sure that every line gets style
		            str = str.replace(/^.*$/gm, function (m) {
		                return '<span class="reset font ' + css + '">' + m + '</span>';
		            });

		        return str;
		    },

		    /**
		    * Pads number with zeros until it's length is the same as given length.
		    * 
		    * @param {Number} number	Number to pad.
		    * @param {Number} length	Max string length with.
		    */
		    padNumber: function (number, length) {
		        var result = number.toString();

		        while (result.length < length)
		            result = '0' + result;

		        return result;
		    },

		    /**
		    * Measures width of a single space character.
		    * 
		    * @private
		    * @return {Number} Returns width of a single space character.
		    */
		    measureSpace: function () {
		        var span = document.createElement("span");

		        span.innerHTML = "&nbsp;";
		        span.className = "dp-highlighter reset font";

		        document.body.appendChild(span);

		        var result = 0;

		        if (/opera/i.test(navigator.userAgent)) {
		            var style = window.getComputedStyle(span, null);
		            result = parseInt(style.getPropertyValue("width"));
		        }
		        else {
		            result = span.offsetWidth;
		        }

		        document.body.removeChild(span);
		        return result;
		    },

		    /**
		    * Replaces tabs with smart spaces. Each tab is assumed to be 4 spaces.
		    * 
		    * @private
		    * 
		    * @param {String} code    Code to fix the tabs in.
		    * @return {String}        Returns code with all tabs replaces with roper amount of spaces.
		    */
		    processSmartTabs: function (code) {
		        var lines = code.split('\n');
		        var result = '';
		        var tabSize = 4;
		        var tab = '\t';

		        // This function inserts specified amount of spaces in the string
		        // where a tab is while removing that given tab.
		        function insertSpaces(line, pos, count) {
		            var left = line.substr(0, pos);
		            var right = line.substr(pos + 1, line.length); // pos + 1 will get rid of the tab
		            var spaces = '';

		            for (var i = 0; i < count; i++)
		                spaces += ' ';

		            return left + spaces + right;
		        }

		        // This function process one line for 'smart tabs'
		        function processLine(line, tabSize) {
		            if (line.indexOf(tab) == -1)
		                return line;

		            var pos = 0;

		            while ((pos = line.indexOf(tab)) != -1) {
		                // This is pretty much all there is to the 'smart tabs' logic.
		                // Based on the position within the line and size of a tab,
		                // calculate the amount of spaces we need to insert.
		                var spaces = tabSize - pos % tabSize;

		                line = insertSpaces(line, pos, spaces);
		            }

		            return line;
		        }

		        // Go through all the lines and do the 'smart tabs' magic.
		        for (var i = 0; i < lines.length; i++)
		            result += processLine(lines[i], tabSize) + '\n';

		        return result;
		    },

		    // copies all <link rel="stylesheet" /> from 'target' window to 'dest'
		    copyStyles: function (destDoc, sourceDoc) {
		        var links = sourceDoc.getElementsByTagName('link');

		        for (var i = 0; i < links.length; i++)
		            if (links[i].rel.toLowerCase() == 'stylesheet')
		                destDoc.write('<link type="text/css" rel="stylesheet" href="' + links[i].href + '"></link>');
		    },

		    fixForBlogger: function (str) {
		        return (dp.sh.isBloggerMode == true) ? str.replace(/<br\s*\/?>|&lt;br\s*\/?&gt;/gi, '\n') : str;
		    },

		    /**
		    * Removes all white space at the begining and end of a string.
		    * 
		    * @private
		    * @param {String} str   String to trim.
		    * @return {String}      Returns string without leading and following white space characters.
		    */
		    trim: function (str) {
		        return str.replace(/\s*$/g, '').replace(/^\s*/, '');
		    },

		    /**
		    * Unindents a block of text by the lowest common indent amount.
		    * 
		    * @private
		    * @param {String} str   Text to unindent.
		    * @return {String}      Returns unindented text block.
		    */
		    unindent: function (str) {
		        var lines = dp.sh.Utils.fixForBlogger(str).split('\n');
		        var indents = new Array();
		        var regex = new RegExp('^\\s*', 'g');
		        var min = 1000;

		        // go through every line and check for common number of indents
		        for (var i = 0; i < lines.length && min > 0; i++) {
		            if (dp.sh.Utils.trim(lines[i]).length === 0)
		                continue;

		            var matches = regex.exec(lines[i]);

		            if (matches != null && matches.length > 0)
		                min = Math.min(matches[0].length, min);
		        }

		        // trim minimum common number of white space from the begining of every line
		        if (min > 0)
		            for (var i = 0; i < lines.length; i++)
		                lines[i] = lines[i].substr(min);

		        return lines.join('\n');
		    }
		}, // end of dp.sh.Utils

	    // Common reusable regular expressions
	    RegexLib:
		{
		    MultiLineCComments: /\/\*[\s\S]*?\*\//gm,
		    SingleLineCComments: /\/\/.*$/gm,
		    SingleLinePerlComments: /#.*$/gm,
		    DoubleQuotedString: /"(?:\.|(\\\")|[^\""\n])*"/g,
		    SingleQuotedString: /'(?:\.|(\\\')|[^\''\n])*'/g,
		    url: /\w+:\/\/[\w-.\/?%&=]*/g
		}, // end of dp.sh.RegexLib

	    /**
	    * This object is populated by user included external brush files.
	    */
	    Brushes: {},

	    BloggerMode: function () {
	        dp.sh.isBloggerMode = true;
	    },

	    /**
	    * Callback method for Array.sort() which sorts matches by
	    * index position and then by length.
	    * 
	    * @private
	    * 
	    * @param {Match} m1	Left object.
	    * @param {Match} m2    Right object.
	    * @return {Number}     Returns -1, 0 or -1 as a comparison result.
	    */
	    matchesSortCallback: function (m1, m2) {
	        // sort matches by index first
	        if (m1.index < m2.index)
	            return -1;
	        else if (m1.index > m2.index)
	            return 1;
	        else {
	            // if index is the same, sort by length
	            if (m1.length < m2.length)
	                return -1;
	            else if (m1.length > m2.length)
	                return 1;
	        }

	        return 0;
	    },

	    /**
	    * Executes given regular expression on provided code and returns all
	    * matches that are found.
	    * 
	    * @private
	    * 
	    * @param {String} code    Code to execute regular expression on.
	    * @param {RegExp} regex   Regular expression to execute.
	    * @param {Object} css     Class name associated with the current regular expression.
	    * @return {Array}         Returns a list of Match objects.
	    */
	    getMatches: function (code, regex, css) {
	        var index = 0;
	        var match = null;
	        var result = [];

	        while ((match = regex.exec(code)) != null)
	            result.push(new dp.sh.Match(match[0], match.index, css));

	        return result;
	    }
	} // end of dp.sh.Utils
};

// make an alias to the actual namespace
dp.SyntaxHighlighter = dp.sh;

//
// Match object
//
dp.sh.Match = function (value, index, css) {
    this.value = value;
    this.index = index;
    this.length = value.length;
    this.css = css;
};

dp.sh.Match.prototype = {
    toString: function () {
        return this.value;
    }
};

/**
* Main Highlither class.
* 
* @constructor
*/
dp.sh.Highlighter = function () {
    this.wrapColumn = 80;

    this.params = {};
    this.firstLine = 1;
    this.div = null;
    this.lines = null;
    this.code = null;
    this.bar = null;

    /**
    * Width of a single space.
    */
    this.spaceWidth = dp.sh.Utils.measureSpace();
};

dp.sh.Highlighter.prototype = {
    /**
    * Returns value of the parameter passed to the highlighter.
    * 
    * @param {String} name           Name of the parameter.
    * @param {Object} defaultValue   Default value.
    */
    getParam: function (name, defaultValue) {
        var result = this.params[name];
        return result != null ? result : defaultValue;
    },

    /**
    * Creates a new instance of a given element and sets its
    * `highlighter` property to the current class.
    *
    * @private
    * 
    * @param {String} name   Name of the element to create (DIV, A, etc).
    * @return {Element}      Returns new object.
    */
    createElement: function (name) {
        var result = document.createElement(name);
        result.highlighter = this;
        return result;
    },

    /**
    * Creates a new text element, applies given style and adds it
    * to the this.div element.
    * 
    * @private
    * 
    * @param {String} str    Text.
    * @param {String} css    Style name.
    */
    addBit: function (str, css) {
        if (str === null || str.length === 0)
            return;

        var span = this.createElement('SPAN');

        //	str = str.replace(/&/g, '&amp;');
        str = str.replace(/ /g, '&nbsp;');
        str = str.replace(/</g, '&lt;');
        //	str = str.replace(/&lt;/g, '<');
        //	str = str.replace(/>/g, '&gt;');
        str = str.replace(/\n/gm, '&nbsp;<br>');

        // when adding a piece of code, check to see if it has line breaks in it
        // and if it does, wrap individual line breaks with span tags
        if (css != null) {
            if ((/br/gi).test(str)) {
                var lines = str.split('&nbsp;<br>');

                for (var i = 0; i < lines.length; i++) {
                    span = this.createElement('SPAN');
                    span.className = css;
                    span.innerHTML = lines[i];

                    this.div.appendChild(span);

                    // don't add a <BR> for the last line
                    if (i + 1 < lines.length)
                        this.div.appendChild(this.createElement('BR'));
                }
            }
            else {
                span.className = css;
                span.innerHTML = str;
                this.div.appendChild(span);
            }
        }
        else {
            span.innerHTML = str;
            this.div.appendChild(span);
        }
    },

    /**
    * Checks if one match is inside another.
    * 
    * @private 
    * 
    * @param {Match} match   Match object to check.
    * @return {Boolean}      Returns true if given match was inside another, false otherwise.
    */
    isMatchNested: function (match) {
        for (var i = 0; i < this.matches.length; i++) {
            var item = this.matches[i];

            if (item === null)
                continue;

            if ((match.index > item.index) && (match.index < item.index + item.length))
                return true;
        }

        return false;
    },

    /**
    * Applies all regular expression to the code and stores all found
    * matches in the `this.matches` array.
    * 
    * @private
    */
    findMatches: function () {
        var result = [];

        if (this.regexList != null)
            for (var i = 0; i < this.regexList.length; i++) {
                var item = this.regexList[i];
                result = result.concat(dp.sh.getMatches(this.code, item.regex, item.css));
            }

        // sort the matches
        result = result.sort(dp.sh.matchesSortCallback);

        this.matches = result;
    },

    setupRuler: function () {
        var div = this.createElement('div');
        var columns = this.createElement('div');
        var showEvery = 10;
        var i = 1;

        while (i <= 150) {
            if (i % showEvery === 0) {
                div.innerHTML += i;
                i += (i + '').length;
            }
            else {
                div.innerHTML += '&middot;';
                i++;
            }
        }

        columns.className = 'columns';
        columns.appendChild(div);
        this.bar.appendChild(columns);
    },

    removeNestedMatches: function () {
        // The following loop checks to see if any of the matches are inside
        // of other matches. This process would get rid of highligted strings
        // inside comments, keywords inside strings and so on.
        for (var i = 0; i < this.matches.length; i++)
            if (this.isMatchNested(this.matches[i]))
                this.matches[i] = null;
    },

    splitIntoDivs: function (code) {
        var lines = code.split(/\n/g);
        var padLength = (this.firstLine + lines.length).toString().length;

        code = '';

        for (var i = 0; i < lines.length; i++) {
            var line = lines[i];
            var indent = /^(&nbsp;)+ /.exec(line);
            var alt = (i % 2 == 0 ? 1 : 2);
            var lineNumber = dp.sh.Utils.padNumber(this.firstLine + i, padLength);

            if (indent != null) {
                line = line.substr(indent[0].length);

                //
                // Six is '&nbsp;'.length.
                //
                // Five is added because last space bar in a sequence is 
                // never converted to &nbsp; so we pad the length with the
                // remaining 5 chanracters.
                // 
                indent = this.spaceWidth * (indent[0].length + 5) / 6;
            }
            else {
                indent = 0;
            }

            if (line.length == 0)
                line = '&nbsp;';

            code +=
				'<div class="font line alt' + alt + '">'
					+ '<div class="number">' + lineNumber + '.</div>'
					+ '<div class="content">'
						+ '<div style="padding-left:' + indent + 'px;">' + line + '</div>'
					+ '</div>'
				+ '</div>'
			;
        }

        return code;
    },

    processMatches: function () {
        // This function returns a portions of the string from pos1 to pos2 inclusive
        function copy(string, pos1, pos2) {
            return string.substr(pos1, pos2 - pos1);
        }

        var pos = 0;
        var code = '';

        // Finally, go through the final list of matches and pull the all
        // together adding everything in between that isn't a match.
        for (var i = 0; i < this.matches.length; i++) {
            var match = this.matches[i];

            if (match === null || match.length === 0)
                continue;

            code += dp.sh.Utils.decorateBit(copy(this.code, pos, match.index), null);
            code += dp.sh.Utils.decorateBit(match.value, match.css);
            pos = match.index + match.length;
        }

        code += dp.sh.Utils.decorateBit(this.code.substr(pos), null);
        code = this.splitIntoDivs(code);
        code = dp.sh.Utils.processUrls(code);

        this.lines.innerHTML = code;
    },


    /**
    * <p>Highlights the code and returns complete HTML.</p>
    * 
    * <p>The following parameters are accepted:</p>
    * <table>
    * <th>
    * 		<td>Name</td>
    * 		<td>Default</td>
    * 		<td>Description</td>
    * </th>
    * <tr>
    * 		<td>collapse</td>
    * 		<td>false</td>
    * 		<td>Makes entire code element invisible by default and adds '+ expand code' button to the tool bar.</td>
    * </tr>
    * </table>
    * 
    *   * test
    *   * haha
    *   
    * @param {String} code     Code to highlight.
    * @param {Object} params   Parameters object.
    */
    highlight: function (code, params) {
        if (code === null)
            code = '';

        if (params != null)
            this.params = params;

        this.originalCode = code;
        this.code = dp.sh.Utils.trim(dp.sh.Utils.unindent(code));
        this.div = this.createElement('DIV');
        this.bar = this.createElement('DIV');
        this.lines = this.createElement('DIV');

        this.div.className = 'dp-highlighter';
        this.bar.className = 'bar';

        if (this.getParam('collapse', false))
            this.div.className += ' collapsed';

        if (this.getParam('noGutter', false))
            this.div.className += ' nogutter';

        // replace tabs with spaces
        if (this.getParam('tabsToSpaces', true))
            this.code = dp.sh.Utils.processSmartTabs(this.code);

        if (this.getParam('addControls', true))
            this.bar.appendChild(dp.sh.Toolbar.create(this));

        // add columns ruler
        if (this.getParam('showColumns', false))
            this.setupRuler();

        this.div.appendChild(this.bar);
        this.div.appendChild(this.lines);

        this.findMatches();
        this.removeNestedMatches();
        this.processMatches();
    },

    /**
    * Converts space separated list of keywords into a regular expression string.
    * 
    * @param {String} str    Space separated keywords.
    * @return {String}       Returns regular expression string.
    */
    getKeywords: function (str) {
        return '\\b' + str.replace(/ /g, '\\b|\\b') + '\\b';
    },

    /**
    * Alias for getKeywords. Kept for compatability with older brushes.
    * @alias getKeywords
    * @deprecated This method is deprecated in favor of Highlighter.getKeywords
    */
    GetKeywords: function (str) {
        return this.getKeywords(str);
    }

}; // end of dp.sh.Highlighter class

// highlightes all elements identified by name and gets source code from specified property
dp.sh.highlight = function (element) {
    var elements = element ? [element] : document.getElementsByTagName('pre'),
		propertyName = 'innerHTML',
		highlighter = null,
		brushes = {}
		;

    if (elements.length === 0)
        return;

    // Find all brushes
    for (var brush in dp.sh.Brushes) {
        var aliases = dp.sh.Brushes[brush].Aliases;

        if (aliases === null)
            continue;

        for (var i = 0; i < aliases.length; i++)
            brushes[aliases[i]] = brush;
    }

    for (var i = 0; i < elements.length; i++) {
        var element = elements[i],
			params = dp.sh.Utils.parseParams(element.className),
			brush = brushes[params['brush']]
			;

        if (brush == null)
            continue;

        // instantiate a brush
        highlighter = new dp.sh.Brushes[brush]();

        // hide the original element
        element.style.display = 'none';

        //			highlighter.noGutter = IsOptionSet('nogutter', options);
        //			highlighter.addControls = !IsOptionSet('nocontrols', options);
        //			highlighter.collapse = IsOptionSet('collapse', options);
        //			highlighter.showColumns = IsOptionSet('showcolumns', options);
        //		highlighter.firstLine = (firstLine === null) ? parseInt(GetOptionValue('firstline', options, 1)) : firstLine;

        highlighter.highlight(element[propertyName], params);
        highlighter.source = element;
        element.parentNode.insertBefore(highlighter.div, element);
    }
}

// Local Variables:
// mode: javascript
// indent-tabs-mode: t
// c-file-style: "stroustrup"
// End: