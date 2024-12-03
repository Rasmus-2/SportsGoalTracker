fetch('/api/chart/data')
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        createChart(data);
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });

