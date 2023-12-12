// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    $( function() {
    var availableTags = [
    "ActionScript",
    "AppleScript",
    "Asp",
    "BASIC",
    "C",
    "C++",
    "Clojure",
    "COBOL",
    "ColdFusion",
    "Erlang",
    "Fortran",
    "Groovy",
    "Haskell",
    "Java",
    "JavaScript",
    "Lisp",
    "Perl",
    "PHP",
    "Python",
    "Ruby",
    "Scala",
    "Scheme"
    ];
    function split( val ) {
      return val.split( /,\s*/ );
    }
    function extractLast( term ) {
      return split( term ).pop();
    }

    $(".autocompletebyid").each(function (i, el) {
        var availableTags = tags[el.id];
        $(el)
    });
    // don't navigate away from the field on tab when selecting an item
    .on( "keydown", function( event ) {
        if ( event.keyCode === $.ui.keyCode.TAB &&
    $( this ).autocomplete( "instance" ).menu.active ) {
        event.preventDefault();
        }
      })
    .autocomplete({
        minLength: 0,
    source: function( request, response ) {
        // delegate back to autocomplete, but extract the last term
        response($.ui.autocomplete.filter(
            availableTags, extractLast(request.term)));
        },
    focus: function() {
          // prevent value inserted on focus
          return false;
        },
    select: function( event, ui ) {
          var terms = split( this.value );
    // remove the current input
    terms.pop();
    // add the selected item
    terms.push( ui.item.value );
    // add placeholder to get the comma-and-space at the end
    terms.push( "" );
    this.value = terms.join( ", " );
    return false;
        }
      });
  } );
</script>
