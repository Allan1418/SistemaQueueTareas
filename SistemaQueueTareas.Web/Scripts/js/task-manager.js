// Enhanced Task Manager JavaScript

document.addEventListener("DOMContentLoaded", () => {
    // Initialize countdown timer
    initializeCountdown()

    // Add glass effect on scroll
    window.addEventListener("scroll", () => {
        const sidebar = document.querySelector(".sidebar")
        if (window.scrollY > 10) {
            sidebar.classList.add("glass-effect")
        } else {
            sidebar.classList.remove("glass-effect")
        }
    })

    // View Toggle
    document.querySelectorAll(".toggle-btn").forEach((btn) => {
        btn.addEventListener("click", function () {
            document.querySelectorAll(".toggle-btn").forEach((b) => b.classList.remove("active"))
            this.classList.add("active")

            const view = this.dataset.view
            const taskList = document.getElementById("tasks-list")

            if (view === "grid") {
                taskList.classList.add("grid-view")
                localStorage.setItem("taskView", "grid")
            } else {
                taskList.classList.remove("grid-view")
                localStorage.setItem("taskView", "list")
            }
        })
    })

    // Restore view preference
    const savedView = localStorage.getItem("taskView")
    if (savedView) {
        const viewBtn = document.querySelector(`.toggle-btn[data-view="${savedView}"]`)
        if (viewBtn) viewBtn.click()
    }

    // Quick Filters
    document.querySelectorAll(".quick-filter-btn").forEach((btn) => {
        btn.addEventListener("click", function () {
            // Toggle active state
            if (this.classList.contains("active") && this.dataset.filter !== "all") {
                this.classList.remove("active")
                document.querySelector('.quick-filter-btn[data-filter="all"]').classList.add("active")
            } else {
                document.querySelectorAll(".quick-filter-btn").forEach((b) => b.classList.remove("active"))
                this.classList.add("active")
            }

            const filter = this.dataset.filter
            filterTasks(filter)
        })
    })

    // Client-side task filtering
    function filterTasks(filter) {
        const taskItems = document.querySelectorAll(".task-item")

        taskItems.forEach((item) => {
            if (filter === "all") {
                item.style.display = ""
                return
            }

            if (filter === "active" && item.classList.contains("active")) {
                item.style.display = ""
            } else if (item.querySelector(`.status-${filter}`)) {
                item.style.display = ""
            } else {
                item.style.display = "none"
            }
        })

        // Check if no tasks are visible and show message
        const visibleTasks = document.querySelectorAll(".task-item[style='']").length
        const noTasksMessage = document.getElementById("no-filtered-tasks")

        if (visibleTasks === 0 && !noTasksMessage) {
            const tasksList = document.querySelector(".tasks-list")
            const emptyMessage = document.createElement("div")
            emptyMessage.id = "no-filtered-tasks"
            emptyMessage.className = "empty-state"
            emptyMessage.innerHTML = `
        <div class="empty-icon">
          <i class="fas fa-filter"></i>
        </div>
        <h4>No hay tareas con este filtro</h4>
        <p>No se encontraron tareas que coincidan con el filtro seleccionado.</p>
        <button class="btn btn-primary reset-filter">
          <i class="fas fa-times"></i>
          Limpiar Filtro
        </button>
      `
            tasksList.appendChild(emptyMessage)

            // Add event listener to reset filter button
            emptyMessage.querySelector(".reset-filter").addEventListener("click", () => {
                document.querySelector('.quick-filter-btn[data-filter="all"]').click()
            })
        } else if (visibleTasks > 0 && noTasksMessage) {
            noTasksMessage.remove()
        }
    }

    // Refresh Tasks List with improved animation
    document.getElementById("refresh-tasks").addEventListener("click", function () {
        const spinner = this.querySelector("i")
        spinner.classList.add("fa-spin")

        // Add loading state to tasks
        const taskItems = document.querySelectorAll(".task-item")
        taskItems.forEach((item) => {
            item.classList.add("loading-shimmer")
        })

        fetch("/Tasks/GetTaskListPartial")
            .then((response) => {
                if (!response.ok) throw new Error("Error en la respuesta")
                return response.text()
            })
            .then((html) => {
                const tasksContainer = document.getElementById("tasks-list")

                // Fade out
                tasksContainer.style.opacity = "0"

                setTimeout(() => {
                    tasksContainer.innerHTML = html

                    // Fade in
                    tasksContainer.style.opacity = "1"
                    showNotification("Lista de tareas actualizada", "success")

                    // Reapply current filter if any
                    const activeFilter = document.querySelector(".quick-filter-btn.active")
                    if (activeFilter && activeFilter.dataset.filter !== "all") {
                        filterTasks(activeFilter.dataset.filter)
                    }

                    // Reinitialize quick filter buttons
                    initQuickFilterButtons()
                }, 300)
            })
            .catch((error) => {
                console.error("Error:", error)
                showNotification("Error al actualizar las tareas", "error")
            })
            .finally(() => {
                spinner.classList.remove("fa-spin")
            })
    })

    // Refresh Active Task with improved animation
    document.getElementById("refresh-active-task").addEventListener("click", function () {
        const spinner = this.querySelector("i")
        spinner.classList.add("fa-spin")

        // Add loading state
        const activeTaskContainer = document.getElementById("active-task-container")
        activeTaskContainer.classList.add("loading-shimmer")

        fetch("/Tasks/GetActiveTaskPartial")
            .then((response) => {
                if (!response.ok) throw new Error("Error en la respuesta")
                return response.text()
            })
            .then((html) => {
                // Fade out
                activeTaskContainer.style.opacity = "0"

                setTimeout(() => {
                    activeTaskContainer.innerHTML = html
                    activeTaskContainer.style.opacity = "1"
                    activeTaskContainer.classList.remove("loading-shimmer")

                    initializeCountdown()
                    showNotification("Tarea activa actualizada", "success")
                }, 300)
            })
            .catch((error) => {
                console.error("Error:", error)
                showNotification("Error al actualizar la tarea activa", "error")
            })
            .finally(() => {
                spinner.classList.remove("fa-spin")
            })
    })

    // Reset Filters
    document.getElementById("reset-filters").addEventListener("click", () => {
        const prioritySelect = document.getElementById("id_priority")
        const stateSelect = document.getElementById("id_state")

        if (prioritySelect) prioritySelect.value = ""
        if (stateSelect) stateSelect.value = ""

        document.getElementById("filters-form").submit()
    })

    // Initialize quick filter buttons
    function initQuickFilterButtons() {
        document.querySelectorAll(".quick-filter-btn").forEach((btn) => {
            btn.addEventListener("click", function () {
                // Toggle active state
                if (this.classList.contains("active") && this.dataset.filter !== "all") {
                    this.classList.remove("active")
                    document.querySelector('.quick-filter-btn[data-filter="all"]').classList.add("active")
                } else {
                    document.querySelectorAll(".quick-filter-btn").forEach((b) => b.classList.remove("active"))
                    this.classList.add("active")
                }

                const filter = this.dataset.filter
                filterTasks(filter)
            })
        })
    }

    // Initialize quick filter buttons on page load
    initQuickFilterButtons()

    // Scroll to active task if exists
    const activeTask = document.querySelector(".task-item.active")
    if (activeTask) {
        setTimeout(() => {
            activeTask.scrollIntoView({ behavior: "smooth", block: "center" })
        }, 500)
    }
})

// Enhanced countdown timer
function initializeCountdown() {
    const countdownElement = document.getElementById("countdown")
    if (!countdownElement) return

    const startTimeStr = countdownElement.getAttribute("data-start")
    if (!startTimeStr) return

    const startTime = new Date(startTimeStr).getTime()
    const progressBar = document.getElementById("progress-bar")

    // Estimated task duration (30 minutes by default)
    const estimatedDuration = 30 * 60 * 1000

    function updateCountdown() {
        const now = new Date().getTime()
        const elapsed = now - startTime
        const remaining = estimatedDuration - elapsed

        if (remaining <= 0) {
            countdownElement.innerHTML = '<i class="fas fa-check-circle"></i> Completado'
            if (progressBar) {
                progressBar.style.width = "100%"
                progressBar.classList.add("success")
            }
            return
        }

        // Format remaining time
        const hours = Math.floor(remaining / (1000 * 60 * 60))
        const minutes = Math.floor((remaining % (1000 * 60 * 60)) / (1000 * 60))
        const seconds = Math.floor((remaining % (1000 * 60)) / 1000)

        let timeDisplay = ""
        if (hours > 0) {
            timeDisplay += `${hours}h `
        }
        timeDisplay += `${minutes}m ${seconds}s`

        countdownElement.innerHTML = `<i class="fas fa-hourglass-half"></i> ${timeDisplay}`

        // Update progress bar
        if (progressBar) {
            const progressPercent = Math.min(100, (elapsed / estimatedDuration) * 100)
            progressBar.style.width = `${progressPercent}%`

            progressBar.classList.remove("warning", "danger")
            if (progressPercent > 75) {
                progressBar.classList.add("danger")
            } else if (progressPercent > 50) {
                progressBar.classList.add("warning")
            }
        }
    }

    // Initial update
    updateCountdown()

    // Update every second
    setInterval(updateCountdown, 1000)
}

// Enhanced notification system
function showNotification(message, type = "info") {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll(".notification")
    existingNotifications.forEach((notification) => {
        notification.classList.remove("show")
        setTimeout(() => notification.remove(), 300)
    })

    const notification = document.createElement("div")
    notification.className = `notification ${type}`

    let icon = "fa-info-circle"
    if (type === "error") icon = "fa-exclamation-circle"
    if (type === "success") icon = "fa-check-circle"

    notification.innerHTML = `
        <div class="notification-content">
            <i class="fas ${icon}"></i>
            <span>${message}</span>
        </div>
        <button class="notification-close">
            <i class="fas fa-times"></i>
        </button>
    `

    document.body.appendChild(notification)

    // Show notification with animation
    setTimeout(() => {
        notification.classList.add("show")
    }, 10)

    // Auto hide after 5 seconds
    setTimeout(() => {
        notification.classList.remove("show")
        setTimeout(() => {
            notification.remove()
        }, 300)
    }, 5000)

    // Close button
    notification.querySelector(".notification-close").addEventListener("click", () => {
        notification.classList.remove("show")
        setTimeout(() => {
            notification.remove()
        }, 300)
    })
}
