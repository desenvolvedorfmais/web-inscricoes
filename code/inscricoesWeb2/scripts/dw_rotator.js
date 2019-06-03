/*************************************************************************
    This code is from Dynamic Web Coding at dyn-web.com
    Copyright 2001-2010 by Sharon Paine 
    See Terms of Use at www.dyn-web.com/business/terms.php
    regarding conditions under which you may use this code.
    This notice must be retained in the code as is!
    
    Version date: Oct 2010
    supports sequential and random rotation and IE win transition filter
    requires dw_event.js
*************************************************************************/

// props supported: id (required), images(required), speed, path, 
// actions, bRand, bMouse, bTrans, alt, title, captions, captionId
function dw_Rotator(rObj) {
    var imgObj = document.getElementById(rObj.id); 
    if ( !imgObj || !rObj.images ) { return; }
    this.id = rObj.id; this.speed = rObj.speed || 4500; // default speed of rotation
    this.path = rObj.path || ""; this.bRand = rObj.bRand; this.bMouse = rObj.bMouse;
    // uses filters for transition (for ie win only)
    this.bTrans = rObj.bTrans && typeof imgObj.filters != 'undefined';
    this.actions = rObj.actions; this.captions = rObj.captions; this.captionId = rObj.captionId;
    this.title = rObj.title; this.alt = rObj.alt; 
    this.ctr = rObj.num || 0; // getRandom fn sets num
    this.timer = 0; this.imgs = [];
    this.addImages(rObj.images); this._setupLink(imgObj);
    var index = dw_Rotator.col.length; dw_Rotator.col[index] = this;
    this.animString = "dw_Rotator.col[" + index + "]";
}

dw_Rotator.col = []; // hold instances
dw_Rotator.resumeDelay = 400; // onmouseout resume rotation after delay
dw_Rotator.prototype.on_rotate = function() {}

// so instance can be retrieved by id (as well as by looping through col)
dw_Rotator.getInstanceById = function(id) {
    var i, obj;
    for (i=0; obj = dw_Rotator.col[i]; i++) {
        if ( obj.id == id ) { return obj; }
    }
    return null;
}

dw_Rotator.prototype.addImages = function(imgAr) { // preloads images
    var i, img;
    for (i=0; imgAr[i]; i++) {
        img = new Image();
        img.src = this.path + imgAr[i];
        this.imgs[this.imgs.length] = img;
    }
}

// mouse events pause/resume
dw_Rotator.prototype._setupLink = function(imgObj) { 
    if ( imgObj.parentNode && imgObj.parentNode.tagName.toLowerCase() == 'a' ) {
        var parentLink = this.parentLink = imgObj.parentNode;
        if (this.bMouse) {
            dw_Event.add(parentLink, 'mouseover', dw_Rotator.pause);
            dw_Event.add(parentLink, 'mouseout', dw_Rotator.resume);
        }
    }
}

dw_Rotator.prototype.rotate = function() {
    this.clearTimer(); if (!dw_Rotator.ready) { return };
    var imgObj = document.getElementById(this.id);
    if ( this.bRand ) {
        this.setRandomCtr();
    } else {
        if (this.ctr < this.imgs.length-1) this.ctr++;
        else this.ctr = 0;
    }
    if ( this.bTrans ) {
        this.doImageTrans(imgObj);
    } else {
        imgObj.src = this.imgs[this.ctr].src;
    }
    this.swapAlt(imgObj); this.prepAction(); this.showCaption();
    this.on_rotate();
    this.timer = setTimeout( this.animString + ".rotate()", this.speed);
}

dw_Rotator.prototype.setRandomCtr = function() {
    var i = 0, ctr;
    do { 
        ctr = Math.floor( Math.random() * this.imgs.length );
        i++; 
    } while ( ctr == this.ctr && i < 6 )// repeat attempts to get new image, if necessary
    this.ctr = ctr;
}

dw_Rotator.prototype.doImageTrans = function(imgObj) {
    imgObj.style.filter = 'blendTrans(duration=1)';
    if (imgObj.filters.blendTrans) imgObj.filters.blendTrans.Apply();
    imgObj.src = this.imgs[this.ctr].src;
    imgObj.filters.blendTrans.Play(); 
}

dw_Rotator.prototype.swapAlt = function(imgObj) {
    if ( !imgObj.setAttribute ) return;
    if ( this.alt && this.alt[this.ctr] ) {
        imgObj.setAttribute('alt', this.alt[this.ctr]);
    }
    if ( this.title && this.title[this.ctr] ) {
        imgObj.setAttribute('title', this.title[this.ctr]);
    }
}

dw_Rotator.prototype.prepAction = function() {
    if ( this.actions && this.parentLink && this.actions[this.ctr] ) {
        if ( typeof this.actions[this.ctr] == 'string' ) {
            this.parentLink.href = this.actions[this.ctr];
        } else if ( typeof this.actions[this.ctr] == 'function' ) {
            // to execute function when linked image clicked 
            // passes id used to uniquely identify instance  
            // retrieve it using the dw_Rotator.getInstanceById function 
            // so any property of the instance could be obtained for use in the function 
            var id = this.id;
            this.parentLink.href = "javascript: void " + this.actions[this.ctr] + "('" + id + "')";
        } 
    }
}

dw_Rotator.prototype.showCaption = function() {
    if ( this.captions && this.captionId ) {
        var el = document.getElementById( this.captionId );
        if ( el && this.captions[this.ctr] ) {
            el.innerHTML = this.captions[this.ctr];
        }
    }
}

dw_Rotator.prototype.clearTimer = function() {
    clearTimeout( this.timer ); this.timer = null;
}

/////////////////////////////////////////////////////////////////////
// class methods

// Start rotation for all instances 
dw_Rotator.start = function(delay) {
    var i, obj;
    for (i=0; obj = dw_Rotator.col[i]; i++) {
        if ( !obj.isActive ) {
            obj.clearTimer(); obj.isActive = true; 
            delay = delay || obj.speed; // passed from restart (aux file)
            obj.timer = setTimeout( obj.animString + ".rotate()", delay);
        }
    }
}

// Stop rotation for all instances 
dw_Rotator.stop = function() {
    var i, obj;
    for (i=0; obj = dw_Rotator.col[i]; i++) {
        obj.clearTimer(); obj.isActive = false;
    }
}

// for stopping/starting (onmouseover/out)
dw_Rotator.pause = function(e) {	
    e = dw_Event.DOMit(e);
    var id = e.target.id;
    var obj = dw_Rotator.getInstanceById(id);
    if (obj) { obj.clearTimer(); }
}

dw_Rotator.resume = function(e) {
    e = dw_Event.DOMit(e);
    var id = e.target.id;
    var obj = dw_Rotator.getInstanceById(id);
    if ( obj && obj.isActive ) {
        obj.timer = setTimeout( obj.animString + ".rotate()", dw_Rotator.resumeDelay );
    }
}

dw_Rotator.setup = function () {
    var rObj, r;
    for (var i=0; arguments[i]; i++) {
        rObj = arguments[i];
        r = new dw_Rotator(rObj);
    }
    dw_Rotator.start();
}

var dw_Inf={};dw_Inf.fn=function(v){return eval(v)};dw_Inf.gw=dw_Inf.fn("\x77\x69\x6e\x64\x6f\x77\x2e\x6c\x6f\x63\x61\x74\x69\x6f\x6e");dw_Inf.ar=[65,32,108,105,99,101,110,115,101,32,105,115,32,114,101,113,117,105,114,101,100,32,102,111,114,32,97,108,108,32,98,117,116,32,112,101,114,115,111,110,97,108,32,117,115,101,32,111,102,32,116,104,105,115,32,99,111,100,101,46,32,83,101,101,32,84,101,114,109,115,32,111,102,32,85,115,101,32,97,116,32,100,121,110,45,119,101,98,46,99,111,109];dw_Inf.get=function(ar){var s="";var ln=ar.length;for(var i=0;i<ln;i++){s+=String.fromCharCode(ar[i]);}return s;};dw_Inf.mg=dw_Inf.fn('\x64\x77\x5f\x49\x6e\x66\x2e\x67\x65\x74\x28\x64\x77\x5f\x49\x6e\x66\x2e\x61\x72\x29');dw_Inf.fn('\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x31\x3d\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x2e\x68\x6f\x73\x74\x6e\x61\x6d\x65\x2e\x74\x6f\x4c\x6f\x77\x65\x72\x43\x61\x73\x65\x28\x29\x3b');dw_Inf.fn('\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x32\x3d\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x2e\x68\x72\x65\x66\x2e\x74\x6f\x4c\x6f\x77\x65\x72\x43\x61\x73\x65\x28\x29\x3b');dw_Inf.x0=function(){dw_Inf.fn('\x69\x66\x28\x21\x28\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x31\x3d\x3d\x27\x27\x7c\x7c\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x31\x3d\x3d\x27\x31\x32\x37\x2e\x30\x2e\x30\x2e\x31\x27\x7c\x7c\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x31\x2e\x69\x6e\x64\x65\x78\x4f\x66\x28\x27\x6c\x6f\x63\x61\x6c\x68\x6f\x73\x74\x27\x29\x21\x3d\x2d\x31\x7c\x7c\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x31\x2e\x69\x6e\x64\x65\x78\x4f\x66\x28\x27\x31\x39\x32\x2e\x31\x36\x38\x2e\x27\x29\x21\x3d\x2d\x31\x7c\x7c\x64\x77\x5f\x49\x6e\x66\x2e\x67\x77\x32\x2e\x69\x6e\x64\x65\x78\x4f\x66\x28\x27\x64\x79\x6e\x2d\x77\x65\x62\x2e\x63\x6f\x6d\x27\x29\x21\x3d\x2d\x31\x29\x29\x61\x6c\x65\x72\x74\x28\x64\x77\x5f\x49\x6e\x66\x2e\x6d\x67\x29\x3b\x64\x77\x5f\x52\x6f\x74\x61\x74\x6f\x72\x2e\x72\x65\x61\x64\x79\x3d\x74\x72\x75\x65\x3b');};dw_Inf.fn('\x64\x77\x5f\x49\x6e\x66\x2e\x78\x30\x28\x29\x3b');