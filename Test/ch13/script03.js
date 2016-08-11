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
		getPix();
	}
	else {
		alert("Sorry, but I couldn't create an XMLHttpRequest");
	}
}

function getPix() {
	xhr.open("GET", "flickrfeed.xml", true);
	xhr.addEventListener("readystatechange",showPictures,false);
	xhr.send(null);

	setTimeout(getPix, 5 * 1000);
}

function showPictures() {
	var tempText = document.createElement("div");
			
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			var allImages = xhr.responseXML.getElementsByTagName("content");
			var randomImg = Math.floor(Math.random() * allImages.length);

			tempText.innerHTML = getPixVal(allImages[randomImg]);
			var thisImg = tempText.getElementsByTagName("p")[1];
			document.getElementById("pictureBar").innerHTML = thisImg.innerHTML;
		}
		else {
			alert("There was a problem with the request " + xhr.status);
		}
	}
	
	function getPixVal(inVal) {
		return (inVal.textContent) ? inVal.textContent : inVal.text;
	}
}
