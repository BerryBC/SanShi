window.addEventListener("load",nameFieldInit,false);

function nameFieldInit() {
	if (document.cookie != "") {
		document.getElementById("nameField").innerHTML = "Hello, " + document.cookie.split("=")[1];
	}
}
