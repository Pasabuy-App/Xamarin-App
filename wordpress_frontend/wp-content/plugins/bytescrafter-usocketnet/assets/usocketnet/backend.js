
// Copy value to clipboard by class of btn.
// btn should have data-clipboard-text="valuehere"
function copyFromId( elemId ) 
{
    var clipboard = new ClipboardJS('.'+elemId);
    console.log("Copied: " + elemId);
}

jQuery(document).ready( function ( $ ) 
{
    // First, compile all users from different games and server instance.
    var usnusers = $('#usn-online-users').DataTable({
        responsive: true,
        "columnDefs": [
            {"className": "dt-center", "targets": "_all"}
        ],
    });

    usnusers.on( 'responsive-resize', function ( e, datatable, columns ) {
        var count = columns.reduce( function (a,b) {
            return b === false ? a+1 : a;
        }, 0 );
    
        console.log( count +' column(s) are hidden' );
    } );
});