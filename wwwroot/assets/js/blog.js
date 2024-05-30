setTimeout(() => {
  handleBlog();
}, 100);

function handleBlog() {
  const form = document.querySelector("#form_blog");
  const tableRender = document.querySelector(".tableRender-crud-blog");

  if (form) {
    form.addEventListener("submit", (e) => {
      e.preventDefault();

      const elements = form.querySelectorAll("[name]");
      const dataBuilder = {};
      elements.forEach((ele) => {
        dataBuilder[ele.name] = ele.value;
      });

      let isValid = true;
      for (const i in dataBuilder) {
        if (!dataBuilder[i]) {
          alert("Hay nhap day du cac truong!");
          isValid = false;
          break;
        }
      }

      if (!isValid) return;

      const dataBuilderFormData = new FormData();
      for (const i in dataBuilder) {
        dataBuilderFormData.append(i, dataBuilder[i]);
      }
      fetch("/System/HandleCreateBlog", {
        method: "POST",
        body: dataBuilderFormData,
      }).then(function (res) {
        const check = confirm("Ban da tao thanh cong!");
        if (check) {
          window.location.reload();
        } else {
          window.location.reload();
        }
      });
    });
  }

  if (tableRender) {
    fetch("/System/GetAllBlog")
      .then((res) => res.json())
      .then((res) => {
        const data = res
          .map(
            (item, index) => `  
            <tr>
                <th scope="row">${index + 1}</th>
                <td>${item.title}</td>
                <td>${item.description}</td>
                <td>
                    <form asp-action="DeleteBlog" action="DeleteBlog" method="post" asp-controller="System">
                        <input type="hidden" name="id" value="${item.id}" /> 
                        <button class="btn btn-primary">Xóa</button>
                    </form>
                </td>
            </tr>`
          )
          .join("");
        tableRender.innerHTML = data;
      });
  }
}
