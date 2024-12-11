function createChart(data) {
    const ctx = document.getElementById('myChart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'line', 
        data: {
            labels: data.map((_, index) => `Day ${index + 1}`), // Create labels for each data point
            datasets: [{
                label: 'Practice Log Statistic in %',
                data: data,
                backgroundColor: 'rgba(0, 128, 0, 0.2)', // Green with 20% opacity
                borderColor: 'rgba(0, 128, 0, 1)',      // Solid green
                borderWidth: 4
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
