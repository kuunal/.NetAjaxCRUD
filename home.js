function getEmployeeList(){
    $.get("http://localhost:3000/employee").done(data=>{
    let output="";    
    for(let record of data){
            output+=`<tr>
                <td>${record.id}</td>
                <td>${record.name}</td>
                <td id="data__buttons">
                    <button class="edit-btn" value=${record.id}> <img src="./images/create-24px.svg" alt="Edit"></button>
                    <button class="delete-btn" value=${record.id}> <img src="./images/delete-24px.svg" alt="Delete"></button>
                </td>
                </tr>`;
            }
            $(".data__list").html(output);
            $("sub").text(`(${data.length} employees)`);
        }
    ).fail(err=> console.log(err))
}

$(document).ready(function()
{
    getEmployeeList();
    $(document).on("click",".edit-btn",function  (){
        console.log("Clickes", $(this).val());
        $(".modal").toggleClass("active");
        $(".main").css("background","rgba(0,0,0,0.1)");
        $(".container").css("background","rgba(255,255,255,0.7)");
        $(".nav").css("pointer-events","none");
    })

    $(document).on("click","#close",function(){
        $(".modal").removeClass("active");
        $(".container").css("background","rgba(255,255,255,1)");
        $(".main").css("background","gainsboro");
        $(".nav").css("pointer-events","auto"); 
    })

    $(document).on("click",".delete-btn",function(){
        let currentBtn = $(this);
        $.ajax({
            url:"http://localhost:3000/employee/"+currentBtn.val(),
            method:"DELETE",
            success:function(){
                $(currentBtn).parent().parent().remove();
            },
            error:function(err){
                alert("Something went wrong! Please try again later")
            }
        })
    })
});    