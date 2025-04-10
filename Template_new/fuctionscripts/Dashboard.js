(function ($) {
    function BindTable() {
        $.ajax({
            url: 'Home/DashboardPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                let actemp = JSON.parse(pageload[0])[0].ACTEMPLOYEE;
                let inactemp = JSON.parse(pageload[1])[0].INACTEMPLOYEE;
                let totemp = JSON.parse(pageload[2])[0].TOTEMPLOYEE;
                
                console.log("actemp:", actemp, "inactemp:", inactemp, "totemp:", totemp); // Debugging

                // Initialize the chart
                dlabMorris.init(actemp, inactemp, totemp);

                Highcharts.chart('container1', {
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 45
                        }
                    },
                    title: {
                        text: 'Employee Details'
                    },
                    subtitle: {
                        text: ''
                    },
                    plotOptions: {
                        pie: {
                            innerSize: 100,
                            depth: 45
                        }
                    },
                    series: [{
                        name: 'Medals',
                        data: [
                            ['Active Employee', actemp],
                            ['Inactive Employee', inactemp],
                            ['Total Employee', totemp],


                        ]
                    }]
                });
                var ContactsChat = JSON.parse(pageload[3]);
                loadContacts(ContactsChat);
                //loadMessages("SATHISH");

            },
            error: function (xhr, status, error) {
                console.error("AJAX Error:", status, error);
            }
        });
    }
    

    function loadContacts(contacts) {
        const contactsList = document.getElementById("contacts-list");
        contactsList.innerHTML = ""; // Clear previous contacts
        
        contacts.forEach(contact => {
            const contactDiv = document.createElement("div");
            contactDiv.classList.add("contact");
            contactDiv.onclick = function () { openChat(contact.name); };

            const borderStyle = contact.ONLINESTATUS === 'ONLINE'
                ? 'border: 4px solid green; border-radius: 50%;'
                : 'border: 4px solid red; border-radius: 50%;';

            // Add notification badge if there are unread messages
            const chatCountBadge = contact.CHATCOUNT > 0
                ? `<span class="chat-badge">${contact.CHATCOUNT}</span>`
                : '';

            contactDiv.innerHTML = `
        <div class="contact-avatar">
            <img src="../RearDoorPhoto/${contact.IMAGE}" alt="Profile" style="${borderStyle}">
            ${chatCountBadge} <!-- Display unread message count -->
        </div>
        <div class="contact-info">
            <div class="contact-name">${contact.name}</div>
            <div class="contact-message">${contact.message}</div>
        </div>
        <div class="contact-time">${contact.time}</div>
    `;
            contactsList.appendChild(contactDiv);
        });
    }



    function openChat(name) {
        document.querySelector('.containerchat').style.display = 'none';
        document.querySelector('.chat-container').style.display = 'block';
        document.querySelector('.chat-title').innerHTML = name;
        //alert("12");
        loadMessages(name)
    }
    



    var dlabMorris = function () {
        var donutChart = function (actemp, inactemp, totemp) {
            if (typeof actemp !== 'number' || typeof inactemp !== 'number' || typeof totemp !== 'number') {
                console.warn("Invalid data: Cannot render donut chart");
                return;
            }

            if (!$('#morris_donught').length) {
                console.warn("Element #morris_donught not found!");
                return;
            }

            Morris.Donut({
                element: 'morris_donught',
                data: [
                    { label: "Active Employees", value: actemp },
                    { label: "Inactive Employees", value: inactemp },
                    { label: "Total Employees", value: totemp }
                ],
                resize: true,
                redraw: true,
                colors: ['#FFA7D7', 'rgb(255, 92, 0)', '#ffaa2b'],
            });
        };

        return {
            init: function (actemp, inactemp, totemp) {
                donutChart(actemp, inactemp, totemp);
            }
        };
    }();

    $(document).ready(function () {
        BindTable();
        
    });

    setInterval(function () {
        loadMessages($("#hdautoid").val());
    }, 3000);

    function loadMessages(contactName) {
        let chatBody = document.querySelector(".chat-body");

        // Get the currently logged-in person from the DOM (assuming #loginuser exists)
        let activeLOG = document.querySelector("#loginuser"); // Selects the first element
        let loginperson = activeLOG ? activeLOG.textContent.trim() : "unknown";

        // Make an AJAX request to load messages from the server
        $.ajax({
            url: '/Home/GetMessages', // Adjust this URL to your MVC controller's action
            type: 'GET',
            data: {
                sender: loginperson,
                receiver: contactName
            },
            success: function (response) {
                // Assuming response is a single message object, not an array
                try {
                    let result = JSON.parse(response);  // Parse the response string into a JSON object

                    // Check if the response contains a valid array of messages
                    if (Array.isArray(result)) {
                        result.forEach(message => {
                            let messageDiv = document.createElement("div");

                            // Apply "sent" or "received" class based on who sent the message
                            messageDiv.classList.add("message", message.Sender === loginperson ? "sent" : "received");

                            // Display the message content and timestamp
                            messageDiv.textContent = `${message.Content} (${message.Timestamp})`;
                            chatBody.appendChild(messageDiv);
                        });

                        // Scroll to the bottom to show the latest message
                        chatBody.scrollTop = chatBody.scrollHeight;
                    } else {
                        console.log("No messages found or invalid response format.");
                    }
                } catch (error) {
                    console.error("Error parsing server response:", error);
                }

            },
            error: function (xhr, status, error) {
                console.error("Error loading messages:", error);
            }
        });
    }


    //weather
    const apiKey = '521c7f47eb02c37217033ca265786ba4';
    const city = 'Nagapattinam';
    const weatherUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city},IN&appid=${apiKey}&units=metric`;
    const forecastUrl = `https://api.openweathermap.org/data/2.5/forecast?q=${city},IN&appid=${apiKey}&units=metric`;

    fetch(weatherUrl)
        .then(response => response.json())
        .then(data => {
            document.getElementById("weather-description").textContent = `Description: ${data.weather[0].description}`;
            document.getElementById("weather-temperature").textContent = `Temperature: ${data.main.temp} °C`;
            document.getElementById("weather-humidity").textContent = `Humidity: ${data.main.humidity}%`;
            document.getElementById("weather-wind").textContent = `Wind Speed: ${(data.wind.speed * 3.6).toFixed(2)} km/h`;
            document.getElementById("weather-sunrise").textContent = `Sunrise: ${new Date(data.sys.sunrise * 1000).toLocaleTimeString()}`;
            document.getElementById("weather-sunset").textContent = `Sunset: ${new Date(data.sys.sunset * 1000).toLocaleTimeString()}`;
        })
        .catch(error => console.error('Error fetching current weather:', error));

    fetch(forecastUrl)
        .then(response => response.json())
        .then(data => {
            let forecastHTML = '';
            let chartData = [];

            data.list.forEach((item, index) => {
                if (index % 8 === 0) {
                    forecastHTML += `<div class='forecast-day'>
                                <p>${new Date(item.dt * 1000).toLocaleDateString()}</p>
                                <p>${item.weather[0].description}</p>
                                <p>${item.main.temp} °C</p>
                            </div>`;
                    chartData.push([new Date(item.dt * 1000).toLocaleDateString(), item.main.temp]);
                }
            });

            document.getElementById("weather-forecast").innerHTML = forecastHTML;

            Highcharts.chart('weatherChart', {
                chart: { type: 'line' },
                title: { text: 'Temperature Trend' },
                xAxis: { categories: chartData.map(data => data[0]) },
                yAxis: { title: { text: 'Temperature (°C)' } },
                series: [{ name: 'Temperature', data: chartData.map(data => data[1]) }]
            });
        })
        .catch(error => console.error('Error fetching weather forecast:', error));



    //proverb
    const proverbUrl = "https://api.allorigins.win/get?url=https://zenquotes.io/api/today";

    async function fetchProverb() {
        try {
            const response = await fetch(proverbUrl);
            const data = await response.json();
            const parsedData = JSON.parse(data.contents);

            if (parsedData && Array.isArray(parsedData) && parsedData.length > 0) {
                document.getElementById("daily-proverb").textContent = `"${parsedData[0].q}"`;
            } else {
                document.getElementById("daily-proverb").textContent = "Stay positive and keep moving forward!";
            }
        } catch (error) {
            console.error("Error fetching proverb:", error);
            document.getElementById("daily-proverb").textContent = "Believe in yourself and all that you are!";
        }
    }

    //document.addEventListener("DOMContentLoaded", fetchProverb);


    document.addEventListener("DOMContentLoaded", function () {
        fetchTamilNews();
        fetchProverb();
    });

    document.addEventListener("DOMContentLoaded", function () {
        const backButton = document.querySelector(".back-button");

        if (backButton) {
            backButton.addEventListener("click", function () {
                document.querySelector(".chat-container").style.display = "none";
                document.querySelector(".containerchat").style.display = "block";
                //BindTable();
            });
        }
    });

    function fetchTamilNews() {
        const apiKey = "pub_72121c7a71ea3ae1312ce838dcd6b3ae5a24c"; // Replace with your actual API key
        const apiUrl = `https://newsdata.io/api/1/news?language=ta&apikey=${apiKey}`;

        fetch(apiUrl)
            .then(response => response.json())
            .then(data => {
                if (data.results && data.results.length > 0) {
                    let newsHtml = "";
                    data.results.forEach(news => {
                        newsHtml += `
                        <div class="news-item" style="border-bottom: 1px solid #ddd; padding: 10px;">
                            <h5>${news.title}</h5>
                            <p>${news.description ? news.description : "No description available"}</p>
                            <a href="${news.link}" target="_blank">Read More</a>
                        </div>`;
                    });
                    document.getElementById("tamil-news").innerHTML = newsHtml;
                } else {
                    document.getElementById("tamil-news").innerHTML = "<p>No news available at the moment.</p>";
                }
            })
            .catch(error => {
                console.error("Error fetching news:", error);
                document.getElementById("tamil-news").innerHTML = "<p>Failed to load news.</p>";
            });
    }


    //for chat code
    

})(jQuery);
