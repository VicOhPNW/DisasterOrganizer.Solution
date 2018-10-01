(function blink() {
    $('.blink_me').fadeOut(3000).fadeIn(3000, blink);
  })();

  $(".meter > span").each(function() {
    $(this)
      .data("origWidth", $(this).width())
      .width(0)
      .animate({
        width: $(this).data("origWidth") // or + "%" if fluid
      }, 1200);
  });

  var xAngle = 0, yAngle = 0;
document.addEventListener('keydown', function(e) {
  switch(e.keyCode) {

    case 37: // left
      yAngle -= 90;
      break;

    case 38: // up
      xAngle += 90;
      break;

    case 39: // right
      yAngle += 90;
      break;

    case 40: // down
      xAngle -= 90;
      break;
  };

  $('cube').style.webkitTransform = "rotateX("+xAngle+"deg) rotateY("+yAngle+"deg)";
}, false);
  
  
  $(document).ready(function(){
  
  var myIndex = 0;
  carousel();
  
  function carousel() {
      var i;
      var x = document.getElementsByClassName("mySlides");
      for (i = 0; i < x.length; i++) {
         x[i].style.display = "none";
      }
      myIndex++;
      if (myIndex > x.length) {myIndex = 1}
      x[myIndex-1].style.display = "block";
      setTimeout(carousel, 3000);
  }
  });