window.addEventListener("load",initAll,false);
var xhr = false;
var xPos, yPos;

function initAll() {
	var allLinks = document.getElementsByTagName("a");

	for (var i=0; i< allLinks.length; i++) {
		allLinks[i].addEventListener("mouseover",getPreview,false);
	}
}

function getPreview(evt) {
	if (evt) {
		var url = evt.target;
	}
	else {
		evt = window.event;
		var url = evt.srcElement;
	}
	xPos = parseInt(evt.clientX);
	yPos = parseInt(evt.clientY);
	
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
		alert("Sorry, but I couldn't create an XMLHttpRequest");
	}
}

function showContents() {
	var prevWin = document.getElementById("previewWin");
	
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			prevWin.innerHTML = xhr.responseText;
		}
		else {
			prevWin.innerHTML = "There was a problem with the request " + xhr.status;
		}
		prevWin.style.top = yPos+2 + "px";
		prevWin.style.left = xPos+2 + "px";
		prevWin.style.visibility = "visible";

		prevWin.addEventListener("mouseout", function() {prevWin.style.visibility = "hidden";}, false);
	}
}