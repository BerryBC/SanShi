window.addEventListener("load",rolloverInit,false);
var rolloverFound;

function rolloverInit() {
	for (var i=0; i<document.images.length; i++) {
		if (document.images[i].parentNode.tagName.toLowerCase() == "a") {
			setupRollover(document.images[i]);
		}
	}
}

function setupRollover(theImage) {
	var re = /\s*_off\s*/;
	
	rolloverFound = false;	
	if (theImage.src.indexOf("_off")) {
		findImage(theImage.src.replace(re,"_on"));
	}
	
	if (!rolloverFound) {
		return;
	}

	theImage.outImage = new Image();
	theImage.outImage.src = theImage.src;
	theImage.addEventListener("mouseout", function() {this.src = this.outImage.src;}, false);

	theImage.overImage = new Image();
	theImage.overImage.src = theImage.src.replace(re,"_on");
	theImage.addEventListener("mouseover", function() {this.src = this.overImage.src;}, false);

	theImage.clickImage = new Image();
	theImage.clickImage.src = theImage.src.replace(re,"_click");
	theImage.addEventListener("click", function() {this.src = this.clickImage.src;}, false);

	theImage.parentNode.childImg = theImage;
	theImage.parentNode.addEventListener("blur", function() {this.childImg.src = this.childImg.outImage.src;}, false);
	theImage.parentNode.addEventListener("focus", function() {this.childImg.src = this.childImg.overImage.src;}, false);
}

function findImage(url) {
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
		xhr.addEventListener("readystatechange",picExists,false);
		xhr.open("GET", url, false);
		xhr.send(null);
	}
}

function picExists() {
	console.log(xhr.readyState+"</br>"+xhr.status)
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			rolloverFound = true;
		}
	}
}