$( window ).resize(function() {
/*js for vAlignMiddle div height*/
var vAlignMiddle = $( window ).height();
$('.vAlignMiddle').css('height',vAlignMiddle);
}).resize();



$( document ).ready(function() {
/*js for checkbox*/
  $("#remember").change(function() {
      $('#remember + label').toggleClass("checked", this.checked);
  });
});