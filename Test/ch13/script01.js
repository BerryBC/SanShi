window.addEventListener("load",initAll,false);
var xhr = false;

function initAll() {
	document.getElementById("makeTextRequest").addEventListener("click",getNewFile,false);
	document.getElementById("makeXMLRequest").addEventListener("click",getNewFile,false);
}

function getNewFile(evt) {
	makeRequest(this.href);
	evt.preventDefault();
}

function makeRequest(url) {
	if (window.XMLHttpRequest) {
		xhr = new XMLHttpRequest();
	}
	else {
		if (window.ActiveXObject) {
			try {
				xhr = new ActiveXObject("Microsoft.XMLHTTP");
			}
			catch (e) {
			}
		}
	}

	if (xhr) {
		xhr.addEventListener("readystatechange",showContents,false);
		xhr.open("GET", url, true);
		xhr.send(null);
	}
	else {
		document.getElementById("updateArea").innerHTML = "Sorry, but I couldn't create an XMLHttpRequest";
	}
}

function showContents() {
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			if (xhr.responseXML && xhr.responseXML.childNodes.length > 0) {
				var outMsg = getText(xhr.responseXML.getElementsByTagName("choices")[0]);
			}
			else {
				var outMsg = xhr.responseText;
			}
		}
		else {
			var outMsg = "There was a problem with the request " + xhr.status;
		}
		document.getElementById("updateArea").innerHTML = outMsg;
	}
	
	function getText(inVal) {
		if (inVal.textContent) {
			return inVal.textContent;
		}
		return inVal.text;
	}
}