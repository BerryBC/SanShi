window.addEventListener("load",initAll,false);
var xhr = false;

function initAll() {
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
		xhr.addEventListener("readystatechange",showPictures,false);
		xhr.open("GET", "flickrfeed.xml", true);
		xhr.send(null);
	}
	else {
		alert("Sorry, but I couldn't create an XMLHttpRequest");
	}
}

function showPictures() {
	var tempText = document.createElement("div");
	var theText;
			
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			var allImages = xhr.responseXML.getElementsByTagName("content");

			for (var i=0; i<allImages.length; i++) {
				tempText.innerHTML = getPixVal(allImages[i]);

				theText = tempText.getElementsByTagName("p")[1].innerHTML;
				theText = theText.replace(/240/g,"75");
				theText = theText.replace(/180/g,"75");
				theText = theText.replace(/_m/g,"_s");
				document.getElementById("pictureBar").innerHTML += theText;
			}
		}
		else {
			alert("There was a problem with the request " + xhr.status);
		}
	}
	
	function getPixVal(inVal) {
		return (inVal.textContent) ? inVal.textContent : inVal.text;
	}
}
