async function handleRev() {
    const params = new URLSearchParams();
    const inputMonth = document.querySelector('#input-month');
    params.set('targetMonth', inputMonth.value)

    try {
        const resp = await fetch('/api/OfficeReservations/GetRevenueOfMonth?' + params.toString())
        const respJson = await resp.json();
        console.log('data from server', respJson);
        document.querySelector('.revenue-area').innerHTML = "Expected revenue on month " + inputMonth.value + " is: " + respJson;
    } catch (e) {
        console.error(e);
    }

}

async function handleCap() {
    const params = new URLSearchParams();
    const inputMonth = document.querySelector('#input-month');
    params.set('targetMonth', inputMonth.value)

    try {
        const resp = await fetch('/api/OfficeReservations/GetTotalUnreservedOffices?' + params.toString())
        const respJson = await resp.json();
        console.log('data from server', respJson);
        document.querySelector('.capacity-area').innerHTML = "Expected total capacity of the unreserved offices of month " + inputMonth.value + " is: " + respJson;
    } catch (e) {
        console.error(e);
    }

}

